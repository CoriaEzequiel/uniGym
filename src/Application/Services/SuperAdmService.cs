using Application.Interfaces;
using Application.Mappings;
using Application.Models.Request;
using Application.Models.Response;
using Domain.Interfaces;

namespace Application.Services
{
    public class SuperAdm : ISuperAdm
    {
        private readonly ISuperAdmRepository _superAdminRepository;

        public SuperAdm(ISuperAdmRepository superAdminRepository)
        {
            _superAdminRepository = superAdminRepository;
        }

        

        public SuperAdmResponse? GetSuperAdminById(int id)
        {
            var superAdmin = _superAdminRepository.GetSuperAdmById(id);

            if (superAdmin != null)
            {
                return SuperAdmProfile.ToSuperAdminResponse(superAdmin);
            }

            return null;
        }

        public void CreateSuperAdm(SuperAdmRequest entity)
        {
            var superAdminEntity = SuperAdmProfile.ToSuperAdminEntity(entity);
            _superAdminRepository.AddSuperAdm(superAdminEntity);
        }

        public bool UpdateSuperAdm(int id, SuperAdmRequest superAdmin)
        {
            var superAdminEntity = _superAdminRepository.GetSuperAdmById(id);

            if (superAdminEntity != null)
            {

                if (!string.IsNullOrEmpty(superAdmin.UserName) && superAdmin.UserName != "string")
                {
                    superAdminEntity.UserName = superAdmin.UserName;
                }

                if (!string.IsNullOrEmpty(superAdmin.Password) && superAdmin.Password != "string")
                {
                    superAdminEntity.Password = superAdmin.Password;
                }

                if (!string.IsNullOrEmpty(superAdmin.FirstName) && superAdmin.FirstName != "string")
                {
                    superAdminEntity.FirstName = superAdmin.FirstName;
                }

                if (!string.IsNullOrEmpty(superAdmin.LastName) && superAdmin.LastName != "string")
                {
                    superAdminEntity.LastName = superAdmin.LastName;
                }

                if (superAdmin.Dni != 0)
                {
                    superAdminEntity.Dni = superAdmin.Dni;
                }

                if (!string.IsNullOrEmpty(superAdmin.Email) && superAdmin.Email != "string")
                {
                    superAdminEntity.Email = superAdmin.Email;
                }
                _superAdminRepository.UpdateSuperAdm(superAdminEntity);

                return true;
            }

            return false;
        }



        public bool DeleteSuperAdm(int id)
        {
            var superAdmin = _superAdminRepository.GetSuperAdmById(id);

            if (superAdmin != null)
            {
                _superAdminRepository.DeleteSuperAdm(superAdmin);

                return true;
            }

            return false;
        }
    }
}