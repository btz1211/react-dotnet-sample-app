import { useFetchHouse, useUpdateHouse } from "../../models/HouseHooks";
import HouseForm from "./HouseForm";
import APIStatus from "../util/APIStatus";
import { useParams } from "react-router-dom";
import ValidationSummary from "../util/ValidationSummary";

const HouseEdit = () => {
  const { id } = useParams();
  const updateHouseMutation = useUpdateHouse();

  if (!id) {
    throw new Error("House id cannot be null");
  }

  const { data, status, isSuccess } = useFetchHouse(id);

  if (!isSuccess) {
    return <APIStatus status={status} />;
  }

  if (!data) {
    return <div>House with ${id} was not found</div>;
  }

  return (
    <>
      {updateHouseMutation.isError && (
        <ValidationSummary error={updateHouseMutation.error} />
      )}
      <HouseForm
        house={data}
        submitted={(h) => updateHouseMutation.mutate(h)}
      />
    </>
  );
};

export default HouseEdit;
