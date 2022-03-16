import { Kafic } from "./kafic.js";
import { Sto } from "./sto.js";
import { Stolica } from "./stolica.js";

fetch("https://localhost:5001/Kafic/PreuzmiKafic").then((p) => {
  p.json().then((data) => {
    data.forEach((kafic) => {
      const kafic1 = new Kafic(kafic.id, kafic.ime);
      kafic.stolovi.forEach((k) => {
        const sto = new Sto(k.id, k.oznaka);
        console.log(sto);
        k.stolice.forEach((st) => {
          const stolica = new Stolica(st.id, st.oznaka, st.slobodna);
          sto.stolice.push(stolica);
        });
        kafic1.stolovi.push(sto);
      });
      console.log(kafic1);
      kafic1.drawCoffeeShop(document.body);
    });
  });
});
