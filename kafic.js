import { Narudzbina } from "./narudzbina.js";
import { Proizvod } from "./proizvod.js";
import { Sto } from "./sto.js";
import { Stolica } from "./stolica.js";
export class Kafic {
  constructor(id, naziv) {
    this.id = id;
    this.naziv = naziv;
    this.stolovi = [];
    this.narudzbine = [];
    this.container = null;
  }
  drawCoffeeShop(host) {
    if (!host) throw new Exception("Roditeljski element ne postoji");
    this.container = document.createElement("div");
    this.container.className = "CoffeeShopContainer";
    this.drawForm(this.container);
    this.drawTables(this.container);
    host.appendChild(this.container);
  }
  addTable(top, right, bottom, left, host) {
    let lab = this.container.querySelector(".stoOznaka");
    if (top == 0 && right == 0 && bottom == 0 && left == 0) {
      alert("Nedovoljno stolica");
    } else if (top < 0 || right < 0 || bottom < 0 || left < 0) {
      alert("Negativan broj stolica");
    } else if (lab == null || lab.value == "") {
      alert("Unesite oznaku stola");
    } else if (this.stolovi.filter((s) => s.oznaka == lab.value).length > 0) {
      alert("Oznaka vec postoji");
    } else {
      let s = new Sto(0, lab.value);
      for (let i = 0; i < top; i++) {
        s.stolice.push(new Stolica(0, "T" + (i + 1), true, null));
      }
      for (let i = 0; i < right; i++) {
        s.stolice.push(new Stolica(0, "R" + (i + 1), true, null));
      }
      for (let i = 0; i < bottom; i++) {
        s.stolice.push(new Stolica(0, "B" + (i + 1), true, null));
      }
      for (let i = 0; i < left; i++) {
        s.stolice.push(new Stolica(0, "L" + (i + 1), true, null));
      }

      fetch("https://localhost:5001/Kafic/DodajSto?kaficId=" + this.id, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          oznaka: s.oznaka,
          stolice: s.stolice,
        }),
      })
        .then((p) => {
          if (p.ok) {
            p.json().then((data) => {
              s.id = data.id;
              if (s.stolice.length == data.stolice.length) {
                for (let i = 0; i < s.stolice.length; i++) {
                  s.stolice[i].id = data.stolice[i].id;
                }
                this.stolovi.push(s);
                s.drawTable(host);
              } else {
                alert("greska broj stolica se ne poklapa");
              }
            });
          } else if (p.status == 400) {
            alert("Greska");
          } else {
            alert("Greška prilikom upisa.");
          }
        })
        .catch((p) => {
          alert("Greška prilikom upisa.");
        });
    }
  }
  drawForm(host) {
    if (!host) throw new Exception("Roditeljski element ne postoji");
    let form = document.createElement("div");
    form.className = "form";
    host.appendChild(form);
    let h4 = document.createElement("h4");
    let span = document.createElement("span");
    span.innerHTML = "Add table";
    h4.appendChild(span);
    form.appendChild(h4);
    let addTable = document.createElement("div");
    addTable.className = "formAddTableDiv";
    form.appendChild(addTable);
    let tablecont = document.createElement("div");
    let table = document.createElement("table");
    let tr = document.createElement("tr");
    let td = document.createElement("td");
    let inputL = document.createElement("input");
    let inputR = document.createElement("input");
    let inputB = document.createElement("input");
    let inputT = document.createElement("input");
    inputL.type = "number";
    inputR.type = "number";
    inputB.type = "number";
    inputT.type = "number";
    tr.appendChild(td.cloneNode());
    tr.appendChild(td);
    tr.appendChild(td.cloneNode());
    td.appendChild(inputT);
    table.appendChild(tr);
    tr = document.createElement("tr");
    td = document.createElement("td");
    td.appendChild(inputL);
    tr.appendChild(td);

    td = document.createElement("td");

    let div = document.createElement("div");
    div.className = "addTable";
    td.appendChild(div);
    tr.appendChild(td);

    td = document.createElement("td");
    td.appendChild(inputR);
    tr.appendChild(td);

    td.appendChild(inputR);
    table.appendChild(tr);

    tr = document.createElement("tr");
    td = document.createElement("td");

    tr.appendChild(td.cloneNode());
    tr.appendChild(td);
    tr.appendChild(td.cloneNode());
    td.appendChild(inputB);
    table.appendChild(tr);

    tablecont.appendChild(table);
    addTable.appendChild(tablecont);

    let button = document.createElement("button");
    button.className = "formbutton";
    button.innerHTML = "Add table";
    button.onclick = (ev) => {
      let c = this.container.querySelector(".shop");
      this.addTable(inputT.value, inputR.value, inputB.value, inputL.value, c);
    };
    let labelT = document.createElement("input");
    labelT.className = "stoOznaka";
    labelT.type = "text";
    form.appendChild(labelT);

    form.appendChild(button);

    button = document.createElement("button");
    button.className = "formbutton";
    button.innerHTML = "Add chairs";
    button.onclick = (ev) => {
      let pronadjen = false;
      if (labelT.value == "") {
        alert("Unesite oznaku stola");
      } else {
        for (const [i, sto] of this.stolovi.entries()) {
          if (sto.oznaka === labelT.value) {
            pronadjen = true;
            sto.addChair(
              inputT.value,
              inputB.value,
              inputL.value,
              inputR.value
            );
            break;
          }
        }
        if (pronadjen == false) {
          alert("Sto nije pronadjen");
        }
      }
    };
    form.appendChild(button);

    h4 = document.createElement("h4");
    span = document.createElement("span");
    span.innerHTML = "Remove table";
    h4.appendChild(span);
    form.appendChild(h4);

    let label = document.createElement("input");
    label.type = "text";
    form.appendChild(label);
    button = document.createElement("button");
    button.className = "formbutton";
    button.innerHTML = "Remove table";
    button.onclick = (ev) => {
      let pronadjen = false;
      if (label.value == "") {
        alert("Unesite oznaku stola");
      } else {
        for (const [i, sto] of this.stolovi.entries()) {
          if (sto.oznaka === label.value) {
            pronadjen = true;
            fetch("https://localhost:5001/Kafic/IzbrisiSto/" + sto.id, {
              method: "DELETE",
            })
              .then((p) => {
                if (p.ok) {
                  console.log(sto);
                  sto.removeTable();
                  this.stolovi.splice(i, 1);
                } else if (p.status == 400) {
                  alert("Sto ne postoji!");
                }
              })
              .catch((p) => {
                alert(p);
              });
            break;
          }
        }
        if (pronadjen == false) {
          alert("Sto nije pronadjen");
        }
      }
    };
    form.appendChild(button);
    h4 = document.createElement("h4");
    span = document.createElement("span");
    span.innerHTML = "Get orders";
    h4.appendChild(span);
    form.appendChild(h4);
    button = document.createElement("button");
    button.className = "formbutton";
    button.innerHTML = "Get orders";
    button.onclick = (ev) => {
      this.getOrders(this.container);
    };
    form.appendChild(button);
  }
  drawTables(host) {
    if (!host) throw new Exception("Roditeljski element ne postoji");
    let shop = document.createElement("div");
    shop.className = "shop";
    host.appendChild(shop);
    this.stolovi.forEach((s) => {
      s.drawTable(shop);
    });
  }
  getOrders(host) {
    if (!host) throw new Exception("Roditeljski element ne postoji");
    fetch("https://localhost:5001/Kafic/PreuzmiPorudzbine?id=" + this.id).then(
      (p) => {
        p.json().then((data) => {
          this.narudzbine = [];
          data.forEach((n) => {
            let narudzbina = new Narudzbina(n.id, n.stolicaOznaka, n.stoOznaka);
            n.proizvodi.forEach((p) => {
              let proizvod = new Proizvod("", p.naziv, p.kolicina, p.cena);
              narudzbina.proizvodi.push(proizvod);
            });
            this.narudzbine.push(narudzbina);
          });
          this.drawOrders(host);
        });
      }
    );
  }
  drawOrders(host) {
    if (!host) throw new Exception("Roditeljski element ne postoji");
    let o = this.container.querySelector(".orderscontainer");
    if (o != null) {
      o.remove();
    }
    let orderscontainer = document.createElement("div");
    orderscontainer.className = "orderscontainer";
    //container.appendChild(orderscontainer);
    let span = document.createElement("span");
    span.innerHTML = "ORDERS";
    span.className = "ordersspan";
    orderscontainer.appendChild(span);
    let orders = document.createElement("div");
    orders.className = "orders";

    this.narudzbine.forEach((n) => {
      console.log(n);
      let o = orderscontainer.querySelector(".N" + n.stoOznaka);
      if (o == null) {
        let tableorders = document.createElement("div");
        tableorders.classList.add("tableorders", "N" + n.stoOznaka);
        orderscontainer.appendChild(orders);
        orders.appendChild(tableorders);
        span = document.createElement("span");
        span.innerHTML = "Table " + n.stoOznaka;
        span.classList.add("orderTablename");
        let tableordersbody = document.createElement("div");
        tableordersbody.className = "tableordersbody";
        n.drawOrder(tableordersbody);
        tableorders.appendChild(span);
        tableorders.appendChild(tableordersbody);
      } else {
        let t = o.querySelector(".tableordersbody");
        if (t != null) {
          n.drawOrder(t);
        }
      }
    });

    let button = document.createElement("button");
    button.className = "orderclosebutton";
    button.innerHTML = "Close";
    orderscontainer.appendChild(button);
    button.onclick = (ev) => {
      orderscontainer.remove();
    };
    // this.container.appendChild(container);
    host.appendChild(orderscontainer);
  }
}
