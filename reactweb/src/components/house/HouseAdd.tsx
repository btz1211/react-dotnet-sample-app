import HouseForm from "./HouseForm";
import { House } from "../../models/House";
import { useNavigate } from "react-router-dom";
import { useAddHouse } from "../../models/HouseHooks";
import ValidationSummary from "../util/ValidationSummary";

const HouseAdd = () => {
  const nav = useNavigate();
  const addHouseMutation = useAddHouse();

  const house: House = {
    address: "",
    country: "",
    description: "",
    price: 0,
    id: 0,
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
