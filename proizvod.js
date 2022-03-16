export class Proizvod {
  constructor(id, naziv, kolicina, cena) {
    this.id = id;
    this.naziv = naziv;
    this.kolicina = kolicina;
    this.cena = cena;
    this.slika = "";
  }

  createMenu(host) {
    if (!host) throw new Exception("Roditeljski element ne postoji");
    let item = document.createElement("div");
    item.className = "item";

    let slikatext = document.createElement("div");
    slikatext.className = "slikatext";

    let itemimg = document.createElement("img");
    itemimg.src = this.slika;

    let cena_naziv = document.createElement("div");
    cena_naziv.className = "cena_naziv";

    let spannaziv = document.createElement("span");
    spannaziv.className = "naziv";
    spannaziv.innerHTML = this.naziv;
    let spancena = document.createElement("span");
    spancena.className = "cena";
    spancena.innerHTML = this.cena;

    let kolicina = document.createElement("div");
    kolicina.className = "kolicina";

    let buttonplus = document.createElement("button");
    buttonplus.className = "buttonplus";
    buttonplus.innerHTML = "+";
    let spankolicina = document.createElement("span");
    spankolicina.className = "spankolicina";
    spankolicina.innerHTML = this.kolicina;
    let buttonminus = document.createElement("button");
    buttonminus.className = "buttonminus";
    buttonminus.innerHTML = "-";

    buttonplus.onclick = (ev) => {
      spankolicina.innerHTML = ++this.kolicina;
    };
    buttonminus.onclick = (ev) => {
      if (this.kolicina != 0) {
        spankolicina.innerHTML = --this.kolicina;
      }
    };
    item.appendChild(slikatext);
    item.appendChild(kolicina);
    slikatext.appendChild(itemimg);
    slikatext.appendChild(cena_naziv);
    cena_naziv.appendChild(spannaziv);
    cena_naziv.appendChild(spancena);
    kolicina.appendChild(buttonminus);
    kolicina.appendChild(spankolicina);
    kolicina.appendChild(buttonplus);
    host.appendChild(item);
  }
}
