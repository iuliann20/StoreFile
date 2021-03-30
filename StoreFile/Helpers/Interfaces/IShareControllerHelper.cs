using StoreFile.Models;
using StoreFile.TL.DTO;
using System.Collections.Generic;

namespace StoreFile.Helpers.Interfaces
{
    public interface IShareControllerHelper
    {
        List<SharedFileViewModel> BuildViewModel(List<StoredFileDTO> sharedFileDTOs);
        SharedFileViewModel BuildViewModel(StoredFileDTO sharedFileDTO);
        StoredFileDTO BuildDTO(SharedFileViewModel sharedFileViewModel);

        string FormatSize(long bytes);
    }


}