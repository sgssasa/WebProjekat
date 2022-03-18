import { Proizvod } from "./proizvod.js";
export class Stolica {
  constructor(id, oznaka, slobodna) {
    this.id = id;
    this.oznaka = oznaka;
    this.slobodna = slobodna;
    this.proizvodi = null;
    this.menuContainer = null;
    this.chair = null;
  }
  drawChair(host, classname) {
    if (!host) throw new Exception("Roditeljski element ne postoji");
    this.chair = document.createElement("div");
    this.chair.onclick = (ev) => {
      fetch("https://localhost:5001/Kafic/PreuzmiMeni").then((p) => {
        p.json().then((data) => {
          this.proizvodi = [];
          data.forEach((proizvod) => {
            let p = new Proizvod(proizvod.id, proizvod.naziv, 0, proizvod.cena);
            p.slika = proizvod.slika;
            this.proizvodi.push(p);
          });
          this.drawMenu(document.body);
        });
      });
    };
    this.chair.classList.add(
      "chair",
      classname + (this.slobodna == false ? "red" : "")
    );
    host.appendChild(this.chair);
  }

  drawMenu(host) {
    if (!host) throw new Exception("Roditeljski element ne postoji");
    if (this.menuContainer != null) {
      this.menuContainer.remove();
    }
    this.menuContainer = document.createElement("div");
    this.menuContainer.className = "disableclick";
    let menucontainer = document.createElement("div");
    this.menuContainer.appendChild(menucontainer);
    menucontainer.className = "menucontainer";
    menucontainer.classList.add("menucontainer");
    //let menubody = document.createElement("div");
    // menubody.className = "menucontainerbody";
    let h2 = document.createElement("h2");
    let spanMenu = document.createElement("span");
    spanMenu.innerHTML = "Menu";
    h2.appendChild(spanMenu);
    let items = document.createElement("div");
    items.className = "items";
    menucontainer.appendChild(h2);
    //menucontainer.appendChild(menubody);
    menucontainer.appendChild(items);

    this.proizvodi.forEach((p) => {
      p.createMenu(items);
    });
    let poruci = document.createElement("button");
    poruci.innerHTML = "Order";
    poruci.className = "poruci";
    menucontainer.appendChild(poruci);
    poruci.onclick = (ev) => {
      if (this.proizvodi.filter((p) => p.kolicina > 0).length > 0) {
        fetch("https://localhost:5001/Kafic/Naruci?stolicaId=" + this.id, {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(
            this.proizvodi
              .map((e) => ({ id: e.id, kolicina: e.kolicina }))
              .filter((p) => p.kolicina > 0)
          ),
        })
          .then((p) => {
            if (p.ok) {
              this.menuContainer.remove();
              this.chairRed_Blue("","red");
            } else if (p.status == 400) {
              alert("Porudzbina nije pronadjena.");
            } else {
              alert("Greška prilikom upisa.");
            }
            return 0;
          })
          .catch((p) => {
            alert("Greška prilikom upisa." + p);
          });
      } else {
        this.menuContainer.remove();
      }
    };
    host.appendChild(this.menuContainer);
  }
  chairRed_Blue(p1,p2)
  {
    if (this.chair.classList.contains("chairleft"+p1)) {
      this.chair.classList.remove("chairleft"+p1);
      this.chair.classList.add("chairleft"+p2);
      this.slobodna = false;
    } else if (this.chair.classList.contains("chairright"+p1)) {
      this.chair.classList.remove("chairright"+p1);
      this.chair.classList.add("chairright"+p2);
      this.slobodna = false;
    } else if (this.chair.classList.contains("chairtop"+p1)) {
      this.chair.classList.remove("chairtop"+p1);
      this.chair.classList.add("chairtop"+p2);
      this.slobodna = false;
    } else if (this.chair.classList.contains("chairbottom"+p1)) {
      this.chair.classList.remove("chairbottom"+p1);
      this.chair.classList.add("chairbottom"+p2);
      this.slobodna = false;
    }
  }

}
