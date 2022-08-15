import Header from "./Header";
import HouseList from "../house/HouseList";
import HouseDetail from "../house/HouseDetail";
import "./App.css";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import HouseAdd from "../house/HouseAdd";
import HouseEdit from "../house/HouseEdit";

function App() {
  return (
    <BrowserRouter>
        <div className="container">
          <Header subtitle="" />
          <Routes>
            <Route path="/" element={<HouseList />}></Route>
            <Route path="/house/:id" element={<HouseDetail />}></Route>
            <Route path="/house/add" element={<HouseAdd />}></Route>
            <Route path="/house/edit/:id" element={<HouseEdit />}></Route>

          </Routes>
        </div>
    </BrowserRouter>

  );
}

export default App;
