using Abp.EntityFramework.Entities;
using Lte.MySqlFramework.Abstract;

namespace Lte.Evaluations.DataService.Basic
{
    public class BluePrintService
    {
        private readonly IBluePrintRepository _bluePrintRepository;

        public BluePrintService(IBluePrintRepository bluePrintRepository)
        {

            _bluePrintRepository = bluePrintRepository;
        }

        public int SaveVisioPath(string fslNumber, string path)
        {
            var item =
                _bluePrintRepository.FirstOrDefault(
                    x => x.FslNumber == fslNumber && x.Folder == fslNumber && x.FileName == path);
            if (item != null) return _bluePrintRepository.SaveChanges();
            {
                var stat =
                    _bluePrintRepository.FirstOrDefault(
                        x => x.FslNumber == fslNumber && x.Folder == fslNumber && string.IsNullOrEmpty(x.FileName));
                if (stat == null)
                {
                    item = new BluePrint
                    {
                        FslNumber = fslNumber,
                        Folder = fslNumber,
                        FileName = path
                    };
                    _bluePrintRepository.Insert(item);
                }
                else
                {
                    stat.FileName = path;
                }
            }
            return _bluePrintRepository.SaveChanges();
        }
    }
}