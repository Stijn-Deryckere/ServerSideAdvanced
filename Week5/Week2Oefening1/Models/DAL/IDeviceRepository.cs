using System;
namespace Week2Oefening1.Models.DAL
{
    public interface IDeviceRepository : IGenericRepository<Device>
    {
        System.Collections.Generic.IEnumerable<Week2Oefening1.Models.Device> All();
        Week2Oefening1.Models.Device GetByID(object id);
        Week2Oefening1.Models.Device Insert(Week2Oefening1.Models.Device entity);
    }
}
