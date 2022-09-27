import HouseForm from "./HouseForm";
import { House } from "../../models/House";
import { useNavigate } from "react-router-dom";
import { useAddHouse } from "../../models/HouseHooks";
import ValidationSummary from "../util/ValidationSummary";
import { v4 as uuidv4 } from "uuid";

const HouseAdd = () => {
  const nav = useNavigate();
  const addHouseMutation = useAddHouse();

  const house: House = {
    address: "",
    country: "",
    description: "",
    price: 0,
    id: uuidv4(),
    photo: "",
  };
  return (
    <>
      {addHouseMutation.isError && (
        <ValidationSummary error={addHouseMutation.error} />
      )}
      <HouseForm
        house={house}
        submitted={(h) => {
          addHouseMutation.mutate(h);
        }}
      />
    </>
  );
};

export default HouseAdd;
