using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Xml;
using AssistantScrapMechanic.Domain.GameFiles;
using AssistantScrapMechanic.Domain.IntermediateFiles;
using AssistantScrapMechanic.Logic.Mapper.XmlMapper;

namespace AssistantScrapMechanic.GameFilesReader.FileHandlers
{
    public class ImageCutter
    {
        private readonly string _dataGuiDirectory;
        private readonly string _survivalGuiDirectory;
        private readonly string _outputDirectory;

        private const int ImageSize = 96;
        private const string ImageMap = "IconMap.png";
        private const string ImageCoordinatesMap = "IconMap.xml";
        private const string SurvivalImageMap = "IconMapSurvival.png";
        private const string SurvivalImageCoordinatesMap = "IconMapSurvival.xml";

        public ImageCutter(string dataGuiDirectory, string survivalGuiDirectory, string outputDirectory)
        {
            _dataGuiDirectory = dataGuiDirectory;
            _survivalGuiDirectory = survivalGuiDirectory;
            _outputDirectory = outputDirectory;
        }

        public void CutOutImages(Dictionary<string, List<dynamic>> keyValueOfGameItems)
        {
            CutOutDataImages(keyValueOfGameItems);
            CutOutSurvivalImages(keyValueOfGameItems);
        }

        private void CutOutDataImages(Dictionary<string, List<dynamic>> keyValueOfGameItems)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Path.Combine(_dataGuiDirectory, ImageCoordinatesMap));

            List<IconMapCoordinates> coords = new List<IconMapCoordinates>();
            foreach (XmlNode tableItem in doc.DocumentElement.FirstChild.ChildNodes)
            {
                coords.AddRange(tableItem.ChildNodes.MapImageCoordinates());
            }

            CutOutImage(coords, Path.Combine(_dataGuiDirectory, ImageMap), keyValueOfGameItems);
        }

        private void CutOutSurvivalImages(Dictionary<string, List<dynamic>> keyValueOfGameItems)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Path.Combine(_survivalGuiDirectory, SurvivalImageCoordinatesMap));

            List<IconMapCoordinates> coords = new List<IconMapCoordinates>();
            foreach (XmlNode tableItem in doc.DocumentElement.FirstChild.ChildNodes)
            {
                coords.AddRange(tableItem.ChildNodes.MapImageCoordinates());
            }

            CutOutImage(coords, Path.Combine(_survivalGuiDirectory, SurvivalImageMap), keyValueOfGameItems);
        }

        private void CutOutImage(List<IconMapCoordinates> coords, string bitMapPath, Dictionary<string, List<dynamic>> keyValueOfGameItems)
        {

            using Bitmap imageMap = new Bitmap(Image.FromFile(bitMapPath));
            //imageMap.MakeTransparent();
            foreach (IconMapCoordinates imageCoordinates in coords)
            {
                if (!keyValueOfGameItems.ContainsKey(imageCoordinates.ItemId)) continue;

                List<dynamic> items = keyValueOfGameItems[imageCoordinates.ItemId];
                foreach (dynamic item in items)
                {
                    bool isKnownType = item is RecipeLocalised || item is BlockLocalised;
                    if (!isKnownType) continue;

                    string appId = string.Empty;
                    if (item is RecipeLocalised recipeLocalised) appId = recipeLocalised.AppId;
                    if (item is BlockLocalised blockLocalised) appId = blockLocalised.AppId;

                    Bitmap newImage = new Bitmap(ImageSize, ImageSize);
                    newImage.MakeTransparent();
                    using Graphics graphics = Graphics.FromImage(newImage);
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.CompositingMode = CompositingMode.SourceCopy;
                    graphics.DrawImage(imageMap, new Rectangle(0, 0, ImageSize, ImageSize),
                        new Rectangle(imageCoordinates.X, imageCoordinates.Y, ImageSize, ImageSize),
                        GraphicsUnit.Pixel);

                    string outputPath = Path.Combine(_outputDirectory, "img", $"{appId}.png");
                    if (File.Exists(outputPath))
                    {
                        File.Delete(outputPath);
                    }

                    string dirString = outputPath.Substring(0,
                        outputPath.LastIndexOf(@"\", StringComparison.InvariantCultureIgnoreCase));
                    if (!Directory.Exists(dirString))
                    {
                        Directory.CreateDirectory(dirString);
                    }

                    using FileStream output = File.Open(outputPath, FileMode.Create);
                    Encoder qualityParamId = Encoder.Quality;
                    EncoderParameters encoderParameters = new EncoderParameters(1)
                    {
                        Param = { [0] = new EncoderParameter(qualityParamId, 100) }
                    };
                    ImageCodecInfo codec = ImageCodecInfo.GetImageDecoders().FirstOrDefault(c => c.FormatID == ImageFormat.Png.Guid);
                    newImage.Save(output, codec, encoderParameters);
                }
            }
        }
    }
}
