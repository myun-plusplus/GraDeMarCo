using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace GrainDetector
{
    [Serializable]
    public class Workspace
    {
        public ImageOpenOptions ImageOpenOptions;
        public ImageRange ImageRange;
        public PlanimetricCircle Circle;
        public FilterOptions FilterOptions;
        public BinarizeOptions BinarizeOptions;
        public GrainDetectOptions GrainDetectOptions;
        public DotDrawTool DotInCircleTool;
        public DotDrawTool DotOnCircleTool;
        public DotDrawTool DotDrawTool;
        public DrawnDotsData DrawnDotsData;

        public List<Color> CountedColors;

        public void Save(string filePath)
        {
            byte[] data;

            var formatter = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                formatter.Serialize(ms, this);
                data = new byte[ms.Length];
                data = ms.GetBuffer();
            }

            using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                fs.Write(data, 0, data.Length);
            }
        }

        public void Load(string filePath)
        {
            var fileInfo = new FileInfo(filePath);
            var formatter = new BinaryFormatter();
            Workspace workspace;

            byte[] data = new byte[fileInfo.Length];
            using (var stream = fileInfo.OpenRead())
            {
                stream.Read(data, 0, data.Length);
            }

            using (var ms = new MemoryStream())
            {
                ms.Write(data, 0, data.Length);
                ms.Seek(0, SeekOrigin.Begin);
                workspace = (Workspace)formatter.Deserialize(ms);
            }

            Copy(workspace.ImageOpenOptions, this.ImageOpenOptions);
            Copy(workspace.ImageRange, this.ImageRange);
            Copy(workspace.Circle, this.Circle);
            Copy(workspace.FilterOptions, this.FilterOptions);
            Copy(workspace.BinarizeOptions, this.BinarizeOptions);
            Copy(workspace.GrainDetectOptions, this.GrainDetectOptions);
            Copy(workspace.DotInCircleTool, this.DotInCircleTool);
            Copy(workspace.DotOnCircleTool, this.DotOnCircleTool);
            Copy(workspace.DotDrawTool, this.DotDrawTool);
            Copy(workspace.DrawnDotsData, this.DrawnDotsData);
            this.CountedColors = workspace.CountedColors;
        }

        private static void Copy<T>(T source, T destination)
        {
            var type = typeof(T);
            foreach (var sourceProperty in type.GetProperties())
            {
                var targetProperty = type.GetProperty(sourceProperty.Name);
                targetProperty.SetValue(destination, sourceProperty.GetValue(source));
            }
            foreach (var sourceField in type.GetFields())
            {
                var targetField = type.GetField(sourceField.Name);
                targetField.SetValue(destination, sourceField.GetValue(source));
            }
        }
    }
}
