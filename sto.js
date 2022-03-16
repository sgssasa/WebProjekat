import { Stolica } from "./stolica.js";

export class Sto {
  constructor(id, oznaka) {
    this.id = id;
    this.oznaka = oznaka;
    this.stolice = [];
    this.tableContainer = null;
  }
  removeTable() {
    this.tableContainer.remove();
  }

  drawTable(host) {
    if (!host) throw new Exception("Roditeljski element ne postoji");
    this.tableContainer = document.createElement("div");
    this.tableContainer.className = "tableContainer";
    this.tableContainer.classList.add("tableContainer");
    let tablewithchair = document.createElement("div");
    tablewithchair.className = "tablewithchair";
    this.tableContainer.appendChild(tablewithchair);

    let divleft = document.createElement("div");
    divleft.className = "tableleft";
    let divmiddle = document.createElement("div");
    divmiddle.className = "tablemiddle";
    let divright = document.createElement("div");
    divright.className = "tableright";
    tablewithchair.appendChild(divleft);
    tablewithchair.appendChild(divmiddle);
    tablewithchair.appendChild(divright);
    let middletop = document.createElement("div");
    middletop.className = "tablemiddletop";
    let middlemiddle = document.createElement("div");
    middlemiddle.className = "table";

    middlemiddle.innerHTML = this.oznaka;
    let middlebottom = document.createElement("div");
    middlebottom.className = "tablemiddlebottom";
    divmiddle.appendChild(middletop);
    divmiddle.appendChild(middlemiddle);
    divmiddle.appendChild(middlebottom);

    this.stolice.forEach((s) => {
      if (s.oznaka.includes("T")) {
        s.drawChair(middletop, "chairtop");
      } else if (s.oznaka.includes("R")) {
        s.drawChair(divright, "chairright");
      } else if (s.oznaka.includes("B")) {
        s.drawChair(middlebottom, "chairbottom");
      } else if (s.oznaka.includes("L")) {
        s.drawChair(divleft, "chairleft");
      }
    });
    host.appendChild(this.tableContainer);
  }
  async pom(a) {
    a.id = 10;
  }
  addChair(T, B, L, R) {
    let br = this.stolice.filter((s) => s.oznaka.includes("T")).length;
    let s;
    console.log(T);
    for (let i = 0; i < T; i++) {
      let cont = this.tableContainer.querySelector(".tablemiddletop");
      if (cont != null) {
        s = new Stolica(0, "T" + (br + 1 + i), true);
        this.upisiStolicu(s, cont, "chairtop");
      }
    }
    br = this.stolice.filter((s) => s.oznaka.includes("B")).length;
    for (let i = 0; i < B; i++) {
      let cont = this.tableContainer.querySelector(".tablemiddlebottom");
      if (cont != null) {
        s = new Stolica(0, "B" + (br + 1 + i), true);
        this.upisiStolicu(s, cont, "chairbottom");
      }
    }
    br = this.stolice.filter((s) => s.oznaka.includes("L")).length;
    for (let i = 0; i < L; i++) {
      let cont = this.tableContainer.querySelector(".tableleft");
      if (cont != null) {
        s = new Stolica(0, "L" + (br + 1 + i), true);
        this.upisiStolicu(s, cont, "chairleft");
      }
    }
    br = this.stolice.filter((s) => s.oznaka.includes("R")).length;
    for (let i = 0; i < R; i++) {
      let cont = this.tableContainer.querySelector(".tableright");
      if (cont != null) {
        s = new Stolica(0, "R" + (br + 1 + i), true);
        this.upisiStolicu(s, cont, "chairright");
      }
    }
  }
  upisiStolicu(stolica, container, className) {
    fetch("https://localhost:5001/Kafic/DodajStolicu?stoId=" + this.id, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        oznaka: stolica.oznaka,
        slobodna: stolica.slobodna,
      }),
    })
      .then((p) => {
        if (p.ok) {
          p.json().then((data) => {
            stolica.id = data;
            this.stolice.push(stolica);
            stolica.drawChair(container, className);
          });
        } else if (p.status == 400) {
          alert("Sto nije pronadjen.");
        } else {
          alert("Greška prilikom upisa.");
        }
        return 0;
      })
      .catch((p) => {
        alert("Greška prilikom upisa.");
      });
  }
}
