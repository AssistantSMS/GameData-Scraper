using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Xml;
using AssistantScrapMechanic.Domain.Constant;
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
        private const string CustomizationImageMap = "CustomizationIconMap.png";
        private const string CustomizationImageCoordinatesMap = "CustomizationIconMap.xml";

        public ImageCutter(string dataGuiDirectory, string survivalGuiDirectory, string outputDirectory)
        {
            _dataGuiDirectory = dataGuiDirectory;
            _survivalGuiDirectory = survivalGuiDirectory;
            _outputDirectory = outputDirectory;
        }

        public void CutOutImages(Dictionary<string, List<ILocalised>> keyValueOfGameItems)
        {
            List<string> imageListForPubSpec = new List<string>();
            imageListForPubSpec.AddRange(CutOutDataImages(keyValueOfGameItems));
            imageListForPubSpec.AddRange(CutOutSurvivalImages(keyValueOfGameItems));
            imageListForPubSpec.AddRange(CutOutCustomisationImages(keyValueOfGameItems));

            string outputPath = Path.Combine(_outputDirectory, "pubspecImageList.txt");
            if (File.Exists(outputPath)) File.Delete(outputPath);
            
            imageListForPubSpec.Sort();
            File.WriteAllLines(outputPath, imageListForPubSpec);
        }

        private List<string> CutOutDataImages(Dictionary<string, List<ILocalised>> keyValueOfGameItems)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Path.Combine(_dataGuiDirectory, ImageCoordinatesMap));

            List<IconMapCoordinates> coords = new List<IconMapCoordinates>();
            foreach (XmlNode tableItem in doc.DocumentElement.FirstChild.ChildNodes)
            {
                coords.AddRange(tableItem.ChildNodes.MapImageCoordinates());
            }

            return CutOutImage(coords, Path.Combine(_dataGuiDirectory, ImageMap), keyValueOfGameItems);
        }

        private List<string> CutOutSurvivalImages(Dictionary<string, List<ILocalised>> keyValueOfGameItems)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Path.Combine(_survivalGuiDirectory, SurvivalImageCoordinatesMap));

            List<IconMapCoordinates> coords = new List<IconMapCoordinates>();
            foreach (XmlNode tableItem in doc.DocumentElement.FirstChild.ChildNodes)
            {
                coords.AddRange(tableItem.ChildNodes.MapImageCoordinates());
            }

            return CutOutImage(coords, Path.Combine(_survivalGuiDirectory, SurvivalImageMap), keyValueOfGameItems);
        }

        private List<string> CutOutCustomisationImages(Dictionary<string, List<ILocalised>> keyValueOfGameItems)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Path.Combine(_dataGuiDirectory, CustomizationImageCoordinatesMap));

            List<IconMapCoordinates> coords = new List<IconMapCoordinates>();
            foreach (XmlNode tableItem in doc.DocumentElement.FirstChild.ChildNodes)
            {
                coords.AddRange(tableItem.ChildNodes.MapImageCoordinates());
            }

            return CutOutImage(coords, Path.Combine(_dataGuiDirectory, CustomizationImageMap), keyValueOfGameItems);
        }

        private List<string> CutOutImage(List<IconMapCoordinates> coords, string bitMapPath, Dictionary<string, List<ILocalised>> keyValueOfGameItems)
        {
            List<string> imageListForPubSpec = new List<string>();
            using Bitmap imageMap = new Bitmap(Image.FromFile(bitMapPath));
            //imageMap.MakeTransparent();
            foreach (IconMapCoordinates imageCoordinates in coords)
            {
                if (!keyValueOfGameItems.ContainsKey(imageCoordinates.ItemId)) continue;

                List<ILocalised> items = keyValueOfGameItems[imageCoordinates.ItemId];
                foreach (dynamic item in items)
                {
                    bool isKnownType = item is RecipeLocalised || item is GameItemLocalised || item is CustomisationItemLocalised;
                    if (!isKnownType) continue;

                    //if (item.GetProperty("AppId") != null && item.GetProperty("AppId") != null) continue;

                    string appId = string.Empty;
                    string itemId = string.Empty;
                    if (item is RecipeLocalised recipeLocalised)
                    {
                        appId = recipeLocalised.AppId;
                        itemId = recipeLocalised.ItemId;
                    }
                    if (item is GameItemLocalised blockLocalised)
                    {
                        appId = blockLocalised.AppId;
                        itemId = blockLocalised.ItemId;
                    }
                    if (item is CustomisationItemLocalised customisedLocalised)
                    {
                        appId = customisedLocalised.AppId;
                        itemId = customisedLocalised.ItemId;
                    }

                    if (GuidExclusion.All.Any(a => a.Equals(itemId))) continue;

                    Bitmap newImage = new Bitmap(ImageSize, ImageSize);
                    newImage.MakeTransparent();
                    using Graphics graphics = Graphics.FromImage(newImage);
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.CompositingMode = CompositingMode.SourceCopy;
                    graphics.DrawImage(imageMap, new Rectangle(0, 0, ImageSize, ImageSize),
                        new Rectangle(imageCoordinates.X, imageCoordinates.Y, ImageSize, ImageSize),
                        GraphicsUnit.Pixel);

                    imageListForPubSpec.Add($"assets/img/items/{appId}.png");
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

            return imageListForPubSpec;
        }
    }
}
