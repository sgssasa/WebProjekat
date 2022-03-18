using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KaficController : ControllerBase
    {
        public KaficContext Context { get; set; }
        public KaficController(KaficContext context)
        {
            Context = context;
        }
        [HttpPut]
        [Route("ObradiPorudzbinu")]
        public async Task<IActionResult> ObradiPorudzbinu(int id)
        {
            try
            {
                var p = Context.Porudzbine.Where(x => x.id == id).Include(p=>p.stolica).ThenInclude(p=>p.porudzbine).FirstOrDefault();
                if (p != null)
                {
                    p.obradjena = true;
                    await Context.SaveChangesAsync();
                    if(p.stolica.porudzbine.Where(x=>x.obradjena==false).ToList().Count==0)
                    {
                        p.stolica.slobodna=true;
                        await Context.SaveChangesAsync();
                        return Ok(new {oslobodi=true});
                    }
                   return Ok(new {oslobodi=false});
                }
                else
                {
                    return BadRequest("Porudzbina nije pronađena!");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        [Route("Naruci")]
        public async Task<IActionResult> Naruci(int stolicaId, List<kolicina_proizvod> proizvodi)
        {
            if (stolicaId < 1)
            {
                return BadRequest("Podresan id stolice");
            }
            if (proizvodi.Count == 0)
            {
                return BadRequest("Nedovoljno proizvoda");
            }
            try
            {
                var stolica = Context.Stolice.Where(x => x.id == stolicaId).FirstOrDefault();
                if (stolica == null)
                {
                    return BadRequest("Stolica nije pronadjena");
                }
                stolica.slobodna=false;
                Porudzbina por = new Porudzbina { obradjena = false, stolica = stolica };
                Context.Porudzbine.Add(por);
                await Context.SaveChangesAsync();

                foreach (kolicina_proizvod p in proizvodi)
                {
                    var proizvod = Context.Proizvodi.Where(x => x.id == p.id).FirstOrDefault();
                    if (proizvod != null)
                    {
                        Porudzbenica poru = new Porudzbenica { proizvod = proizvod, porudzbina = por, kolicina = p.kolicina };
                        Context.spojPorudzbine.Add(poru);
                        await Context.SaveChangesAsync();
                    }
                }
                return Ok("Uspesno naruceno");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }


        }

        [HttpGet]
        [Route("PreuzmiPorudzbine")]
        public async Task<IActionResult> PreuzmiPorudzbine(int id)
        {
            if (id < 0)
            {
                return BadRequest("Pogresan id");
            }
            try
            {
                var stolovi = Context.Porudzbine
                   .Include(p => p.stolica).
                   ThenInclude(p => p.sto).Include(q => q.proizvodi).ThenInclude(p => p.proizvod).Where(p => p.obradjena == false && p.stolica.sto.kafic.id == id).Select(p => new
                   {
                       id = p.id,
                       stoOznaka = p.stolica.sto.oznaka,
                       stolicaOznaka = p.stolica.oznaka,
                       proizvodi = p.proizvodi.Select(x => new { naziv = x.proizvod.naziv, cena = x.proizvod.cena, kolicina = x.kolicina })

                   });
                return Ok(await stolovi.ToListAsync());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("PreuzmiKafic")]
        public async Task<IActionResult> PreuzmiKafic()
        {
            try
            {
                var kafic = Context.Kafici
                   .Include(p => p.stolovi).
                   ThenInclude(p => p.stolice);
                return Ok(await kafic.ToListAsync());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        [Route("DodajStolice")]
        public async Task<IActionResult> DodajStolice(Sto s)
        {
            if (string.IsNullOrWhiteSpace(s.oznaka))
            {
                return BadRequest("Oznaka nije validna");
            }
            if (s.oznaka.Length > 10)
            {
                return BadRequest("Oznaka nije validna");
            }
            if (s.stolice.Count == 0)
            {
                return BadRequest("Nema stolica");
            }
            try
            {
                var sto = Context.Stolovi.Where(st => st.id == s.id).FirstOrDefault();
                if (sto != null)
                {
                    foreach (Stolica x in s.stolice)
                    {
                        x.sto = sto;
                        Context.Stolice.Add(x);
                        await Context.SaveChangesAsync();
                    }
                }
                return Ok(s);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        [Route("DodajStolicu")]
        public async Task<IActionResult> DodajStolicu(Stolica s, int stoId)
        {
            if (stoId < 0)
            {
                return BadRequest("Nevalidan id");
            }
            if (string.IsNullOrWhiteSpace(s.oznaka))
            {
                return BadRequest("Oznaka nije validna");
            }
            if (s.oznaka.Length > 10)
            {
                return BadRequest("Oznaka nije validna");
            }
            try
            {
                var sto = Context.Stolovi.Where(st => st.id == stoId).FirstOrDefault();
                if (sto != null)
                {
                    s.sto = sto;
                    Context.Stolice.Add(s);
                    await Context.SaveChangesAsync();
                    return Ok(s.id);
                }
                return BadRequest("Sto nije pronadjen");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
        [HttpPost]
        [Route("DodajSto")]
        public async Task<IActionResult> DodajSto(Sto s, int kaficId)
        {
            if (kaficId < 0)
            {
                return BadRequest("Pogresan id");
            }
            if (s.stolice.Count == 0)
            {
                return BadRequest("Nedovoljan broj stolica");
            }
            if (string.IsNullOrWhiteSpace(s.oznaka))
            {
                return BadRequest("Oznaka nije validna");
            }
            if (s.oznaka.Length > 10)
            {
                return BadRequest("Oznaka nije validna");
            }
            try
            {
                var kafic = Context.Kafici.Where(k => k.id == kaficId).FirstOrDefault();
                if (kafic != null)
                {
                    Sto st = new Sto { oznaka = s.oznaka, kafic = kafic };
                    Context.Stolovi.Add(st);
                    await Context.SaveChangesAsync();

                    foreach (Stolica x in s.stolice)
                    {
                        x.sto = st;
                        Context.Stolice.Add(x);
                    }
                    await Context.SaveChangesAsync();

                    return Ok(st);
                }
                return BadRequest("Kafic nije pronadjen");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [Route("IzbrisiSto/{id}")]
        [HttpDelete]
        public async Task<ActionResult> Izbrisisto(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Pogrešan ID!");
            }
            try
            {
                var sto = await Context.Stolovi.FindAsync(id);
                if (sto == null)
                {
                    return BadRequest("Sto nije pronadjen");
                }
                string oznaka = sto.oznaka;
                Context.Stolovi.Remove(sto);
                await Context.SaveChangesAsync();
                return Ok($"Uspešno izbrisan sto sa oznakom: {oznaka}");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet]
        [Route("PreuzmiMeni")]
        public async Task<IActionResult> PreuzmiMeni()
        {
            try
            {
                var proizvodi = Context.Proizvodi;
                return Ok(await proizvodi.ToListAsync());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut]
        [Route("IzmeniCenu")]
        public async Task<IActionResult> IzmeniCenu([FromBody] Proizvod p)
        {
            if (p.id < 1)
            {
                return BadRequest("Pogresan id");
            }
            if (p.cena < 0)
            {
                return BadRequest("Cena nije validna");
            }
            try
            {
                var proizvod = Context.Proizvodi.Where(pr => pr.id == p.id).FirstOrDefault();
                if (proizvod == null)
                {
                    return BadRequest("Proizvod nije pronadjen");
                }
                proizvod.cena = p.cena;
                await Context.SaveChangesAsync();
                return Ok(proizvod);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
