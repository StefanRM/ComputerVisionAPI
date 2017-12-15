using System;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.Vision;
using Microsoft.ProjectOxford.Vision.Contract;
using System.IO;
using System.Collections.Generic;

namespace Computer_Vision
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get paths of all image files with .jpg, .jpeg, .png extensions
            string[] imgPathsJPG = Directory.GetFiles(".", "*.jpg");
            string[] imgPathsJPEG = Directory.GetFiles(".", "*.jpeg");
            string[] imgPathsPNG = Directory.GetFiles(".", "*.png");

            // add paths to a string List
            var list = new List<string>();
            list.AddRange(imgPathsJPG);
            list.AddRange(imgPathsJPEG);
            list.AddRange(imgPathsPNG);
            string[] imgPaths = list.ToArray(); // get array from list

            // analyze every image
            for (int i = 0; i < imgPaths.Length; i++)
            {
                string folderName, fileName = imgPaths[i];

                Console.WriteLine("\n{0}", imgPaths[i]);
                try
                {
                    // run API
                    AnalysisResult compVisResult = null;
                    Task.Run(async () =>
                    {
                        compVisResult = await UploadAndAnalyzeImage(imgPaths[i]);
                    }).GetAwaiter().GetResult();

                    if (compVisResult.Tags.Length > 0)
                    {
                        string finalResultName = compVisResult.Tags[0].Name;
                        double maxResultConfidence = compVisResult.Tags[0].Confidence;

                        for (int j = 0; j < compVisResult.Tags.Length; j++)
                        {
                            Console.WriteLine("{0} {1}", compVisResult.Tags[j].Name, compVisResult.Tags[j].Confidence);
                            if (maxResultConfidence < compVisResult.Tags[j].Confidence)
                            {
                                maxResultConfidence = compVisResult.Tags[j].Confidence;
                                finalResultName = compVisResult.Tags[j].Name;
                            }
                        }

                        folderName = finalResultName;

                        Console.WriteLine("Final: {0} {1}", finalResultName, maxResultConfidence);
                    }
                    else
                    {
                        Console.WriteLine("Tagless");
                        folderName = "Tagless";
                    }

                    // Create folder if it doesn't already exists
                    string pathString = System.IO.Path.Combine(".", folderName);
                    System.IO.Directory.CreateDirectory(pathString);

                    // move file from current directory to the folder that matches its Tag.Name
                    string initialPathString = System.IO.Path.Combine(".", fileName);
                    pathString = System.IO.Path.Combine(pathString, fileName);
                    File.Move(initialPathString, pathString);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Oops... {0}!\nMove on ;)", ex.Message);
                }
            }
        }

        // Microsoft Vision API function
        public static async Task<AnalysisResult> UploadAndAnalyzeImage(string imageFilePath)
        {
            await Task.Delay(1000);
            VisionServiceClient VisionServiceClient = new VisionServiceClient("1b1fde153ba546dbbc6676d168854b1e", "https://westcentralus.api.cognitive.microsoft.com/vision/v1.0");

            using (Stream imageFileStream = File.OpenRead(imageFilePath))
            {
                VisualFeature[] visualFeatures = new VisualFeature[] { VisualFeature.Adult, VisualFeature.Categories, VisualFeature.Color, VisualFeature.Description, VisualFeature.Faces, VisualFeature.ImageType, VisualFeature.Tags };
                AnalysisResult analysisResult = await VisionServiceClient.AnalyzeImageAsync(imageFileStream, visualFeatures);
                return analysisResult;
            }
        }
    }
}