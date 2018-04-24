using ED.Application.Dtos;

namespace ED.Application.ServiceContract
{
    public interface IPermissionService
    {
        GetResults<BaseEntityDto> GetPermissions(PageInput input);

        UpdateResult UpdatePermission(BaseEntityDto action);

        CreateResult<int> CreatePermission(BaseEntityDto action);

        DeleteResult DeletePermission(int actionId);
    }
}