using StoreFile.Helpers.Interfaces;
using StoreFile.Models;
using StoreFile.TL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StoreFile.Helpers.Classes
{

    public class ShareControllerHelper : IShareControllerHelper
    {
        private static readonly string[] SUFFIXES = { "Bytes", "KB", "MB", "GB", "TB", "PB" };

        public List<SharedFileViewModel> BuildViewModel(List<StoredFileDTO> sharedFileDTOs)
        {
            return sharedFileDTOs.Select(sharedFileDTO => new SharedFileViewModel
            {
                Id = sharedFileDTO.Id,
                FileName = sharedFileDTO.FileName,
                FileSize = sharedFileDTO.FileSize,
                UploadDate = sharedFileDTO.UploadDate
            }).ToList();
        }

        public SharedFileViewModel BuildViewModel(StoredFileDTO sharedFileDTO)
        {
            return new SharedFileViewModel
            {
                Id = sharedFileDTO.Id,
                FileName = sharedFileDTO.FileName,
                FileSize = sharedFileDTO.FileSize,
                UploadDate = sharedFileDTO.UploadDate
            };
        }

        public StoredFileDTO BuildDTO(SharedFileViewModel sharedFileViewModel)
        {
            return new StoredFileDTO
            {
                Id = sharedFileViewModel.Id,
                FileName = sharedFileViewModel.FileName,
                FileSize = sharedFileViewModel.FileSize,
                UploadDate = sharedFileViewModel.UploadDate
            };
        }
        public string FormatSize(long bytes)
        {
            int counter = 0;
            decimal number = bytes;
            while (Math.Round(number / 1024) >= 1)
            {
                number /= 1024;
                counter++;
            }

            return $"{number:n1} {SUFFIXES[counter]}";
        }


    }
}
