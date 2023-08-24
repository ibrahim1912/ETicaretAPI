using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using ETicaretAPI.Application.Abstraction.Storage.Cloudinary;
using ETicaretAPI.Infrastructure.Services.Storage.Cloudinary;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace ETicaretAPI.Infrastructure.Services.Storage
{
    public class CloudinaryStorage : Storage, ICloudinaryStorage
    {
        private IOptions<CloudinarySettings> _cloudinaryConfig;
        CloudinaryDotNet.Cloudinary _cloudinary;

        public CloudinaryStorage(IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _cloudinaryConfig = cloudinaryConfig;
            Account account = new(_cloudinaryConfig.Value.CloudName, _cloudinaryConfig.Value.ApiKey, _cloudinaryConfig.Value.ApiSecret);
            _cloudinary = new CloudinaryDotNet.Cloudinary(account);

        }

        public async Task DeleteAsync(string folderPath, string fileName)
        {
            //await _cloudinary.DeleteResourcesAsync(folderPath, fileName);
            var deletionParams = new DeletionParams($"{folderPath}/{fileName}");

            await _cloudinary.DestroyAsync(deletionParams);
        }

        public List<string> GetFiles(string folderPath)
        {
            // return _cloudinary.ListResources(pathOrContainerName).Resources.Select(f => f.DisplayName).ToList();

            List<string> fileNames = new List<string>();

            var resources = _cloudinary.ListResourcesByPrefix(folderPath);

            foreach (var resource in resources.Resources)
            {
                string fileName = Path.GetFileName(resource.PublicId);
                fileNames.Add(fileName);
            }

            return fileNames;

        }

        public bool HasFile(string folderPath, string fileName)
        {
            var resources = _cloudinary.ListResourcesByPrefix(folderPath);

            return resources.Resources.Any(resource => resource.PublicId ==
            $"{folderPath}/{fileName}");

        }

        public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string folderPath, IFormFileCollection files)
        {
            List<(string fileName, string pathOrContainerName)> data = new();
            foreach (var file in files)
            {
                string fileNewName = await FileRenameAsync(folderPath, file.Name, HasFile);


                await using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams()
                {

                    File = new FileDescription(fileNewName, stream),
                    Folder = folderPath,
                    PublicId = fileNewName

                };
                var uploadResult = _cloudinary.Upload(uploadParams);
                //data.Add((fileNewName, $"{folderPath}\\{fileNewName}"));
                data.Add((fileNewName, $"{folderPath}/{fileNewName}"));
            }

            return data;
        }
    }
}

