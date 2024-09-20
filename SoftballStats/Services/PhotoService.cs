using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.Extensions.Options;
using SoftballStats.Interfaces;
using SoftballStats.Helpers;

namespace SoftballStats.Services
{
    
    public class PhotoService : IPhotoService
    {
        // member variable for cloudinary DI
        private readonly Cloudinary _cloudinary;

        // constructor to implement Dependency Injection
        public PhotoService(IOptions<CloudinarySettings> config)
        {
            var acc = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
                );

            // set the cloudinary member variable
            _cloudinary = new Cloudinary(acc);
        } // end constructor

        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            // create a new image upload result
            var uploadResult = new ImageUploadResult();

            // check if the file is not null
            if (file.Length > 0)
            {
                // open the file stream
                using var stream = file.OpenReadStream();

                // create a new image upload params
                var uploadParams = new ImageUploadParams
                {
                    // set the file name and stream
                    File = new FileDescription(file.FileName, stream),

                    // set the transformation
                    Transformation = new Transformation().Height(500).Crop("fill").Gravity("face")
                };

                // upload the image
                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }

            // return the upload result
            return uploadResult;
        } // end add photo async

        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            // create a new deletion params
            var deleteParams = new DeletionParams(publicId);

            // delete the image
            var result = await _cloudinary.DestroyAsync(deleteParams);

            // return the result
            return result;
        } // end delete photo async
    } // end class
}// end namespace

