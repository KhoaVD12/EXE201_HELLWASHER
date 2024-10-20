using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Utils
{
    public class ImageService
    {
        private readonly Cloudinary _cloudinary;

        public ImageService()
        {
            // Provide Cloudinary credentials (get from your Cloudinary dashboard)
            var account = new Account(
                "dwdph9tsd",    // Your Cloudinary cloud name
                "391774853411878",       // Your Cloudinary API key
                "nl_AONvwUc0Ap81iI__vzj6l5uQ"     // Your Cloudinary API secret
            );

            _cloudinary = new Cloudinary(account);
        }

        public string UploadImage(string filePath)
        {
            // Check if the file exists at the specified file path
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("The specified file does not exist.", filePath);
            }

            // Optionally, validate file extension (e.g., only allow .jpg, .png, etc.)
            var validExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var fileExtension = Path.GetExtension(filePath).ToLower();

            if (!validExtensions.Contains(fileExtension))
            {
                throw new InvalidOperationException("Invalid file format. Only .jpg, .jpeg, .png, .gif are allowed.");
            }

            // Create an upload parameter to specify the file and options
            var imageName = Path.GetFileNameWithoutExtension(filePath); // Get image name without extension
            var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
            var publicId = $"{imageName}_{timestamp}"; // Create a public ID based on the image name and timestamp

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(filePath),
                PublicId = publicId, // Use the constructed public ID
                Transformation = new Transformation().Width(500).Height(500).Crop("fill")
            };

            // Upload the image to Cloudinary
            var uploadResult = _cloudinary.Upload(uploadParams);

            // Check if the upload was successful
            if (uploadResult.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception($"Image upload failed. Cloudinary error: {uploadResult.Error?.Message}");
            }

            // Return the secure URL of the uploaded image
            return uploadResult.SecureUrl.AbsoluteUri;
        }
        public string UploadImageFromUrl(string imageUrl)
        {
            // Validate the URL (you could add more validation if necessary)
            if (!Uri.IsWellFormedUriString(imageUrl, UriKind.Absolute))
            {
                throw new ArgumentException("The provided image URL is not valid.");
            }

            // Create an upload parameter to specify the file and options
            var imageName = Path.GetFileNameWithoutExtension(imageUrl); // Get image name without extension
            var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
            var publicId = $"{imageName}_{timestamp}"; // Create a public ID based on the image name and timestamp

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(imageUrl),
                PublicId = publicId, // Use the constructed public ID
                Transformation = new Transformation().Width(500).Height(500).Crop("fill")
            };

            // Upload the image to Cloudinary
            var uploadResult = _cloudinary.Upload(uploadParams);

            // Check if the upload was successful
            if (uploadResult.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception($"Image upload failed. Cloudinary error: {uploadResult.Error?.Message}");
            }

            // Return the secure URL of the uploaded image
            return uploadResult.SecureUrl.AbsoluteUri;
        }
    }
}
