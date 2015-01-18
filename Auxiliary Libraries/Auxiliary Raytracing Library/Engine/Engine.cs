using System;
using System.ComponentModel;
using System.Threading;

using Auxiliary.Graphics;
using Auxiliary.MathTools;

namespace Auxiliary.Raytracing
{
    /// <summary> Рассчитывает изображение сцены методом трассировки лучей. </summary>
    public class Engine
    {
        #region Public Fields
        
        /// <summary> Глубина трассировки. </summary>
        public static int TraceDepth = 5;        
        
        /// <summary> Число потоков рендеринга. </summary>
        public static int ThreadsCount = 2;
        
        #endregion
        
        #region Private Fields
        
        /// <summary> Отличная от нуля малая величина. </summary>
        private static Scene Scene;
        
        /// <summary> Отличная от нуля малая величина. </summary>
        private static Raster Raster;
        
        /// <summary> Отличная от нуля малая величина. </summary>
        private static BackgroundWorker Worker;        
                
        /// <summary> Время запуска рассчетов. </summary>
        private static DateTime StartTime;

        /// <summary> Процент выполненной работы. </summary>
        private static float Percentage;    
        
        /// <summary> Объект для синхронизации потоков. </summary>
        private static object Locker = new object();   
        
        #endregion

        #region Public Methods

        /// <summary> Производит расчет изображения сцены. </summary>
        public static void Render(Scene scene, Raster raster, BackgroundWorker worker)
        {
        	// Выполняем подготовительные действия
        	{
	        	// Сохраняем сцену для трассировки
	        	Scene = scene;
	        	
	        	// Сохраняем точечный рисунок для вывода изображения
	        	Raster = raster;
	        	
	        	// Сохраняем объект BackgroundWorker для выполнения расчета в отдельном потоке
	        	Worker = worker;
	        	
	        	// Обнуляем процент выполненной работы
	        	Percentage = 0.0f;	        	      	
        	}
        	
        	// Выполняем вычисления в отдельных потоках
        	{
		        // Создаем необходимое число потоков
		        Thread [] threads = new Thread[ThreadsCount];
		        
		        // Сохраняем время запуска расчетов
		        StartTime = DateTime.Now;  	        
	        	
	        	// Запускаем потоки на выполнение
	        	for (int index = 0; index < ThreadsCount; index++)
	        	{
	        		threads[index] = new Thread(RenderPart);
	        		
	        		threads[index].Start(index);
	        	}
	        	
	        	// Ожидаем окончания работы потоков
	        	foreach (Thread thread in threads)
	        	{
	        		thread.Join();
	        	}
        	}
        }
        
        #endregion
        
        #region Private Methods
        
        /// <summary> Производит расчет части изображения сцены. </summary>
        private static void RenderPart(object index)
        {
        	// Проходим по всем пикселям растра...
            for (int y = 0; y < Raster.Height; y++)
            {
            	for (int x = (int) index; x < Raster.Width; x += ThreadsCount)
                {
                	// Генерируем первичный луч (исходит из камеры в сцену)
                	Ray ray = new Ray(Scene.Camera.Position, Scene.Camera.CalcDirection(x, y));
                    
		        	// Создаем структуру с информацией о пересечении
		        	IntersectInfo intersection = IntersectInfo.None;
		        	
                    // Трассируем лучь вглубь сцены
                    Raytrace(ref ray, ref intersection, 1);

                    lock (Locker)
                    {
                    	// Записываем результат в растр
                    	Raster[x, y] = Vector3D.Clamp(ray.Intensity, 0.0f, 1.0f);
                    }
                }
            	
            	lock (Locker)
            	{
            		// Подсчитываем процент завершенной работы
            		Percentage += 100f / (ThreadsCount * Raster.Height);
            		
            		// Подсчитываем время выполнения расчетов
            		TimeSpan time = DateTime.Now - StartTime;
            		
            		// Сообщаем данные о ходе рассчетов
            		Worker.ReportProgress((int) Percentage, time);
            	}
            }
        }

        /// <summary> Производит трассировку заданного луча. </summary>
        private static Primitive Raytrace(ref Ray ray, ref IntersectInfo intersect, int depth)
        {
        	//////////////////////////////////////////////////////////////////////////////////////////////////////
        	// ШАГ 1 -- НАХОДИМ БЛИЖАЙШУЮ ТОЧКУ ПЕРЕСЕЧЕНИЯ
        	
        	// Предполагаем, что пересечения нет
            Primitive primitive = null;

            // Проверяем каждый объект сцены
            foreach (Primitive prim in Scene.Primitives)
            {
            	// Тестируем луч на пересечение с очередным объектом
                IntersectInfo test = prim.CalcIntersection(ray);
                
                // Если пересечение имеет место...
                if (test.Type != IntersectType.None)
                {
                	//...и его время меньше текущего наилучшего времени
                    if (test.Time < intersect.Time)
                    {
                    	//...то запоминаем данный объект...
                        primitive = prim;

                        //...и информацию о пересечении
                        intersect = test;
                    }
                }
            }
            
            // Если луч не пересекается ни с одним объектом...
            if (IntersectInfo.None == intersect)
            {
            	//...то процесс обрывается
                return null;
            }
            
            // Вычисляем координаты точки пересечения
            Vector3D point = ray.Origin + ray.Direction * intersect.Time;         
            
        	//////////////////////////////////////////////////////////////////////////////////////////////////////
        	// ШАГ 2 -- РАССЧИТЫВАЕМ ПРЯМОЕ ОСВЕЩЕНИЕ ТОЧКИ
        	
            // Вычисляем нормаль в точке пересечения
            Vector3D normal = primitive.CalcNormal(point);        	
            
            // Вычисляем вектор отражения в точке пересечения
            Vector3D reflect = Vector3D.Reflect(ray.Direction, normal);
            
            // Вычисляем вклад каждого источника света
            foreach (Light source in Scene.Lights)
            {
                //////////////////////////////////////////////////////////////////////////////////////////////////
                // ШАГ 2.1 -- ВЫЧИСЛЯЕМ КОЭФФИЦИЕНТ ЗАТЕНЕННОСТИ ТОЧКИ

                // Пусть точка освещена
                float shadowing = 1.0f;

                // Вычисляем вектор направления на источник света
                Vector3D direct = Vector3D.Normalize(source.Position - point);
                
                // Вычисляем расстояние до источника света
                float distance = Vector3D.Distance(source.Position, point);
                
                // Генерируем 'теневой' луч для данного источника света...
                Ray shadowRay = new Ray(point + direct * Util.Epsilon, direct);

                //...и тестируем его на пересечение с каждым объектом сцены
                foreach (Primitive prim in Scene.Primitives)
                {
                	// Получаем информацию о пересечении с очередным объектом
                	IntersectInfo info = prim.CalcIntersection(shadowRay);

                	// Если пересечение имеет место...
                	if (info.Type != IntersectType.None)
                	{
                		//...и его время меньше расстояния до источника света...
                		if (info.Time < distance)
                		{
                			//...то данный источник света невидим из точки пересечения...
                			shadowing = 0.0f;
                			
                			// ...и перебор объектов закончен
                			break;
                		}
                	}
                }

                //////////////////////////////////////////////////////////////////////////////////////////////////
                // ШАГ 2.2 -- ВЫЧИСЛЯЕМ ФОНОВУЮ СОСТАВЛЯЮЩУЮ        	

                if (primitive.Material.Ambiant > Vector3D.Zero)
                {
                    ray.Intensity += primitive.Material.Ambiant * source.AmbiantIntensity;
                }

                //////////////////////////////////////////////////////////////////////////////////////////////////
                // ШАГ 2.3 -- ВЫЧИСЛЯЕМ ДИФФУЗНУЮ СОСТАВЛЯЮЩУЮ                    
                
                // Вычисляем вектор направления на источник света
                Vector3D light = Vector3D.Normalize(source.Position - point);

                if (primitive.Material.Diffuse != Vector3D.Zero)
                {
                    float ndotl = Vector3D.Dot(light, normal);

                    if (ndotl > 0)
                    {
                        ray.Intensity += primitive.Material.Diffuse * source.DiffuseIntensity *
                        	primitive.CalcColor(point, normal) * ndotl * shadowing;


                        //////////////////////////////////////////////////////////////////////////////////////////
                        // ШАГ 2.4 -- ВЫЧИСЛЯЕМ БЛИКОВУЮ СОСТАВЛЯЮЩУЮ   

                        if (primitive.Material.Specular != Vector3D.Zero)
                        {
                            float rdotl = Vector3D.Dot(reflect, light);

                            ray.Intensity += primitive.Material.Specular * source.SpecularIntensity *
                            	(float) Math.Pow(rdotl, primitive.Material.Shininess);
                        }
                    }
                }
            }
            
            
        	//////////////////////////////////////////////////////////////////////////////////////////////////////
        	// ШАГ 3 -- РАССЧИТЫВАЕМ ВТОРИЧНОЕ ОСВЕЩЕНИЕ ТОЧКИ            
            
            if (depth < TraceDepth)
            {
            	//////////////////////////////////////////////////////////////////////////////////////////////////
        		// ШАГ 3.1 -- РАССЧИТЫВАЕМ ВКЛАД ОТРАЖЕННОГО СВЕТА
        		
                if (primitive.Material.ReflectCoeff != Vector3D.Zero)
                {
                	// Строим луч отражения
                    Ray reflectRay = new Ray(point + reflect * Util.Epsilon, reflect);
                    
                    // Информация о пересечении отраженного луча
                    IntersectInfo reflectIntersect = IntersectInfo.None;
                    
                    // Трассируем отраженный луч
                    Raytrace(ref reflectRay, ref reflectIntersect, depth + 1);
                    
                    // Прибавляем интенсивность отраженного луча
                    ray.Intensity += primitive.Material.ReflectCoeff * reflectRay.Intensity;
                }
                
            	//////////////////////////////////////////////////////////////////////////////////////////////////
        		// ШАГ 3.2 -- РАССЧИТЫВАЕМ ВКЛАД ПРЕЛОМЛЕННОГО СВЕТА

                if (primitive.Material.RefractCoeff != Vector3D.Zero)
                {
                    if (intersect.Type == IntersectType.Outer)
                    {
                    	// Вычисляем относительный показатель преломления (воздух - объект)
                        float index = 1.0f / primitive.Material.RefractIndex;

                        // Вычисляем вектор преломления
                        Vector3D refract = Vector3D.Normalize(Vector3D.Refract(ray.Direction, normal, index));
                        
                        // Создаем преломленный луч
                        Ray refractRay = new Ray(point + refract * Util.Epsilon, refract);

                        // Создаем информацию о пересечении
                        IntersectInfo refractIntersect = IntersectInfo.None;

                        // Трассируем преломленный луч
                        Raytrace(ref refractRay, ref refractIntersect, depth + 1);
                        
                        // Прибавляем интенсивность преломленного луча 
                        ray.Intensity += refractRay.Intensity;
                    }
                    else
                    {
                    	// Вычисляем относительный показатель преломления (объект - воздух)
                        float index = primitive.Material.RefractIndex;

                        // Вычисляем вектор преломления
                        Vector3D refract = Vector3D.Normalize(Vector3D.Refract(ray.Direction, -normal, index));

                        // Создаем преломленный луч
                        Ray refractRay = new Ray(point + refract * Util.Epsilon, refract);

                        // Создаем информацию о пересечении
                        IntersectInfo refractIntersect = IntersectInfo.None;

                        // Трассируем преломленный луч
                        Raytrace(ref refractRay, ref refractIntersect, depth + 1);
                        
                        // Прибавляем интенсивность преломленного луча
                        ray.Intensity += refractRay.Intensity;
                    }
                }
            }

            return primitive;
        }
        
        #endregion
    }
}
