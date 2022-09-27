import { useParams, Link, useNavigate } from "react-router-dom";
import { useFetchHouse, useDeleteHouse } from "../../models/HouseHooks";
import APIStatus from "../util/APIStatus";
import { currencyFormatter } from "../../config";
import defaultPhoto from "./resources/defaultPhoto";
import Bids from "../bid/Bids";

const HouseDetail = () => {
  const nav = useNavigate();
  const houseDeleteMutator = useDeleteHouse();
  const { id } = useParams();

  // validate id param and convert to int
  if (!id) throw Error(`House id param is required`);

  // retrieve house data
  const { data, status, isSuccess } = useFetchHouse(id);

  if (!isSuccess) {
    return <APIStatus status={status} />;
  }

  if (!data) {
    return <div>House with ${id} was not found</div>;
  }

  // house was found in
  return (
    <div>
      <div className="row">
        <div className="col-6">
          <div className="row">
            <img
              className="img-fluid"
              src={data.photo ? data.photo : defaultPhoto}
              alt="House"
            ></img>
          </div>
          <div className="row mt-3">
            <div className="col-2">
              <Link
                className="btn btn-primary w-100"
                to={`/house/edit/${data.id}`}
              >
                Edit
              </Link>
            </div>
            <div className="col-2">
              <button
                className="btn btn-danger w-100"
                onClick={() => {
                  if (window.confirm("Are you Sure?")) {
                    houseDeleteMutator.mutate(data);
                  }
                  nav("/");
                }}
              >
                Delete
              </button>
            </div>
          </div>
        </div>
        <div className="col-6">
          <div className="row mt-2">
            <h5 className="col-12">{data.country}</h5>
          </div>
          <div className="row mt-2">
            <h5 className="col-12">{data.address}</h5>
          </div>
          <div className="row mt-2">
            <h2 className="themeFontColor col-12">
              {currencyFormatter.format(data.price)}
            </h2>
          </div>
          <div className="row">
            <div className="col-12 mt-3">{data.description}</div>
          </div>
          <Bids house={data} />
        </div>
        
      </div>
    </div>
  );
};

export default HouseDetail;
