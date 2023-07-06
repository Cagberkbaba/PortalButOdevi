using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;
using WebApplication1.ViewModel;

namespace WebApplication1.Controllers
{
    public class ServisController : ApiController
    {
        DB01Entities db = new DB01Entities();
        SonucModel sonuc = new SonucModel();


        #region Kullanıcı

        [HttpGet]
        [Route("api/kullaniciliste")]
        public List<KullaniciModel> kullaniciListe()
        {
            List<KullaniciModel> liste = db.Kullanici.Select(x => new KullaniciModel()


            {
                kullaniciId = x.kullaniciId,
                kullaniciAd = x.kullaniciAd,
            }).ToList();

            return liste;
        }

        [HttpGet]
        [Route("api/kullanicibyid/{kullaniciId}")]
        public KullaniciModel KullaniciById(int kullaniciId)
        {
            KullaniciModel kayit = db.Kullanici.Where(s => s.kullaniciId == kullaniciId).Select(x => new KullaniciModel()
            {
                kullaniciId = x.kullaniciId,
                kullaniciAd = x.kullaniciAd,
            }).SingleOrDefault();

            return kayit;
        }

        [HttpPost]
        [Route("api/kullaniciekle")]
        public SonucModel KullaniciEkle(KullaniciModel model)
        {
            if (db.Kullanici.Count(s => s.kullaniciAd == model.kullaniciAd) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Grup Başlığı Kayıtlıdır!";
                return sonuc;
            }

            Kullanici yeni = new Kullanici();
            yeni.kullaniciId = model.kullaniciId;
            yeni.kullaniciAd = model.kullaniciAd;
            db.Kullanici.Add(yeni);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Kullanıcı Eklendi";
            return sonuc;
        }

        [HttpPut]
        [Route("api/kullaniciduzenle")]
        public SonucModel KullaniciDuzenle(KullaniciModel model)
        {
            Kullanici kayit = db.Kullanici.Where(s => s.kullaniciId == model.kullaniciId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı";
                return sonuc;
            }
            kayit.kullaniciAd = model.kullaniciAd;


            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kullanıcı Düzenlendi";

            return sonuc;
        }

        [HttpDelete]
        [Route("api/kullanicisil/{kullaniciId}")]
        public SonucModel KullaniciSil(int kullaniciId)
        {
            Kullanici kayit = db.Kullanici.Where(s => s.kullaniciId == kullaniciId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı";
                return sonuc;
            }

            db.Kullanici.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kullanıcı Silindi";
            return sonuc;
        }
        #endregion


        #region Grup


        [HttpGet]
        [Route("api/grupliste")]
        public List<GrupModel> GrupListe()
        {
            List<GrupModel> liste = db.Grup.Select(x => new GrupModel()
            {
                grupId = x.grupId,
                grupAd = x.grupAd,

            }).ToList();

            return liste;
        }
        [HttpGet]
        [Route("api/gruplistesoneklenenler/{s}")]
        public List<GrupModel> GrupListeSonEklenenler(int s)
        {
            List<GrupModel> liste = db.Grup.OrderByDescending(o => o.grupId).Take(s).Select(x => new GrupModel()
            {
                grupId = x.grupId,
                grupAd = x.grupAd,

            }).ToList();

            return liste;
        }
        [HttpGet]
        [Route("api/gruplistebykatid/{katId}")]
        public List<GrupModel> GrupListeByMesajId(int mesajId)
        {
            List<GrupModel> liste = db.Grup.Select(x => new GrupModel()
            {
                grupId = x.grupId,
                grupAd = x.grupAd,
            }).ToList();

            return liste;
        }
        [HttpGet]
        [Route("api/gruplistebykullaniciid/{kullaniciId}")]
        public List<GrupModel> KullaniciListeByKullaniciId(int kullaniciId)
        {
            List<GrupModel> liste = db.Grup.Select(x => new GrupModel()
            {
                grupId = x.grupId,
                grupAd = x.grupAd,

            }).ToList();

            return liste;
        }

        [HttpGet]
        [Route("api/grupbyid/{grupId}")]
        public GrupModel GrupById(int grupId)
        {
            GrupModel kayit = db.Grup.Where(s => s.grupId == grupId).Select(x => new GrupModel()
            {
                grupId = x.grupId,
                grupAd = x.grupAd,
            }).SingleOrDefault();
            return kayit;
        }

        [HttpPost]
        [Route("api/grupekle")]
        public SonucModel GrupEkle(GrupModel model)
        {
            if (db.Grup.Count(s => s.grupAd == model.grupAd) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Grup Başlığı Kayıtlıdır!";
                return sonuc;
            }

            Grup yeni = new Grup();
            yeni.grupId = model.grupId;
            yeni.grupAd = model.grupAd;
            db.Grup.Add(yeni);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Grup Eklendi";
            return sonuc;
        }

        [HttpPut]
        [Route("api/grupduzenle")]
        public SonucModel GrupDuzenle(GrupModel model)
        {
            Grup kayit = db.Grup.Where(s => s.grupId == model.grupId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }
            kayit.grupId = model.grupId;
            kayit.grupAd = model.grupAd;
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Grup Düzenlendi";
            return sonuc;

        }

        [HttpDelete]
        [Route("api/grupsil/{grupId}")]
        public SonucModel GrupSil(int grupId)
        {
            Grup kayit = db.Grup.Where(s => s.grupId == grupId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }

            db.Grup.Remove(kayit);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Grup Silindi";
            return sonuc;
        }
        #endregion


        #region Mesaj


        [HttpGet]
        [Route("api/mesajliste")]
        public List<MesajModel> MesajListe()
        {
            List<MesajModel> liste = db.Mesaj.Select(x => new MesajModel()
            {
                mesajId = x.mesajId,
                mesajText = x.mesajText,
                bulkMesaj = x.bulkMesaj,
                grupId = x.grupId,
                kullaniciAd = x.Kullanici.kullaniciAd,
                kimdenId = x.kimdenId,
                kimeId = x.kimeId,

            }).ToList();

            return liste;
        }
        [HttpGet]
        [Route("api/mesajlistesoneklenenler/{s}")]
        public List<MesajModel> MesajListeSonEklenenler(int s)
        {
            List<MesajModel> liste = db.Mesaj.OrderByDescending(o => o.mesajId).Take(s).Select(x => new MesajModel()
            {
                mesajId = x.mesajId,
                mesajText = x.mesajText,
                bulkMesaj = x.bulkMesaj,
                grupId = x.grupId,
                kullaniciAd = x.Kullanici.kullaniciAd,
                kimdenId = x.kimdenId,
                kimeId = x.kimeId,

            }).ToList();

            return liste;
        }

        [HttpGet]
        [Route("api/mesajbyid/{mesajId}")]
        public MesajModel MesajById(int mesajId)
        {
            MesajModel kayit = db.Mesaj.Where(s => s.mesajId == mesajId).Select(x => new MesajModel()
            {
                mesajId = x.mesajId,
                mesajText = x.mesajText,
                bulkMesaj = x.bulkMesaj,
                grupId = x.grupId,
                kullaniciAd = x.Kullanici.kullaniciAd,
                kimdenId = x.kimdenId,
                kimeId = x.kimeId,
            }).SingleOrDefault();
            return kayit;
        }

        [HttpPost]
        [Route("api/mesajekle")]
        public SonucModel MesajEkle(MesajModel model)
        {

            Mesaj yeni = new Mesaj();
            yeni.mesajId = model.mesajId;
            yeni.mesajText = model.mesajText;
            yeni.bulkMesaj = model.bulkMesaj;
            yeni.grupId = model.grupId;
            yeni.kimdenId = model.kimdenId;
            yeni.kimeId = model.kimeId;


            db.Mesaj.Add(yeni);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Mesaj Gönderildi";
            return sonuc;
        }


        [HttpDelete]
        [Route("api/mesajsil/{mesajId}")]
        public SonucModel MesajSil(int mesajId)
        {
            Mesaj kayit = db.Mesaj.Where(s => s.mesajId == mesajId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }


            db.Mesaj.Remove(kayit);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Mesaj Silindi";
            return sonuc;
        }
        #endregion

    }
}
