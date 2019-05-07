using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JlueCertificate.Bll.Base
{
    public class BaseData
    {
        public static object getCitys()
        {
            List<Entity.Respose.Province> result = new List<Entity.Respose.Province>();
            List<Entity.MsSQL.T_ProvinceCity> _allcitys = Dal.MsSQL.T_ProvinceCity.GetList();
            List<Entity.MsSQL.T_ProvinceCity> _provinces = _allcitys.Where(ii => ii.LevelCode.Length == 3).ToList();
            foreach (var _province in _provinces.OrderBy(ii => ii.LevelCode))
            {
                Entity.Respose.Province _provinceMod = new Entity.Respose.Province()
                {
                    id = _province.CityId,
                    name = _province.CityName
                };
                List<Entity.MsSQL.T_ProvinceCity> _citys = _allcitys.Where(ii => ii.LevelCode.StartsWith(_province.LevelCode) && ii.LevelCode.Length == 6).ToList();
                foreach (var _city in _citys.OrderBy(ii => ii.LevelCode))
                {
                    Entity.Respose.City _cityMod = new Entity.Respose.City()
                    {
                        id = _city.CityId,
                        name = _city.CityName
                    };
                    List<Entity.MsSQL.T_ProvinceCity> _zones = _allcitys.Where(ii => ii.LevelCode.StartsWith(_city.LevelCode) && ii.LevelCode.Length == 9).ToList();
                    foreach (var _zone in _zones.OrderBy(ii => ii.LevelCode))
                    {
                        _cityMod.zones.Add(new Entity.Respose.Zone()
                        {
                            id = _zone.CityId,
                            name = _zone.CityName
                        });
                    }
                    _provinceMod.citys.Add(_cityMod);
                }
                result.Add(_provinceMod);
            }
            return result;
        }

        public static object getOLSchoolSubjects(string _uid, string _pwd, ref string error)
        {
            Entity.MsSQL.T_MarkUser _user = Dal.MsSQL.T_MarkUser.GetModel(_uid, _pwd);
            if (_user != null)
            {
                Entity.Respose.getolschoolsubjects result = new Entity.Respose.getolschoolsubjects();
                List<Entity.Respose.olschoolsubject> _olsub = new List<Entity.Respose.olschoolsubject>();
                _olsub = Dal.MsSQL.T_Subject.GetOLSchoolAllList();
                result.all = _olsub;
                return result;
            }
            else
            {
                error = "账号失效，请重新登陆";
                return "-1";
            }
        }
    }
}
