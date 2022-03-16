import { Proizvod } from "./proizvod.js";

export class Narudzbina {
  constructor(id, stolicaOznaka, stoOznaka) {
    this.id = id;
    this.stolicaOznaka = stolicaOznaka;
    this.stoOznaka = stoOznaka;
    this.proizvodi = [];
    this.container = null;
  }

  drawOrder(host) {
    if (!host) throw new Exception("Roditeljski element ne postoji");
    this.container = document.createElement("div");
    let order = document.createElement("div");
    order.className = "order";
    this.container.appendChild(order);
    let orderheder = document.createElement("div");
    orderheder.className = "orderheder";
    order.appendChild(orderheder);
    //let h4 = document.createElement("h4");
    let span = document.createElement("span");
    span.innerHTML = this.stolicaOznaka;
    span.className = "tablelabelorder";
    // h4.appendChild(span);
    orderheder.appendChild(span);
    let orderitem = null;
    let ime;
    let kolicina;
    this.proizvodi.forEach((p) => {
      orderitem = document.createElement("div");
      orderitem.className = "orderitem";
      ime = document.createElement("span");
      ime.innerHTML = p.naziv;
      ime.className = "orderitemnaziv";
      kolicina = document.createElement("span");
      kolicina.className = "orderitemkolicina";
      kolicina.innerHTML = p.kolicina;
      orderitem.appendChild(ime);
      orderitem.appendChild(kolicina);
      order.appendChild(orderitem);
    });
    let button = document.createElement("button");
    button.className = "servebutton";
    button.innerHTML = "Serve";
    button.onclick = (ev) => {
      fetch("https://localhost:5001/Kafic/ObradiPorudzbinu?id=" + this.id, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
        },
      })
        .then((p) => {
          if (p.ok) {
            if (this.container != null) {
              this.container.remove();
            }
          } else if (p.status == 400) {
            alert("Porudzbina nije pronadjen.");
          } else {
            alert("Greška prilikom upisa.");
          }
          return 0;
        })
        .catch((p) => {
          alert("Greška prilikom upisa.");
        });
    };
    order.appendChild(button);
    host.appendChild(this.container);
  }
}
