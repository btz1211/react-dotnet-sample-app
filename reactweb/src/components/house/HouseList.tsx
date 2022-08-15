import { currencyFormatter } from "../../config";
import APIStatus from "../util/APIStatus";
import { useNavigate } from "react-router-dom";
import { Link } from "react-router-dom";
import { useFetchHouses } from "../../models/HouseHooks"

const HouseList = () => {
  const nav = useNavigate();
  const { data, status, isSuccess } = useFetchHouses();

  // show an API status page while loading
  if (!isSuccess) {
    return <APIStatus status={status} />;
  }

  return (
    <div>
      <div className="row mb-2">
        <h5 className="themeFontColor text-center">
          Houses currently on the market
        </h5>
      </div>
      <table className="table table-hover">
        <thead>
          <tr>
            <th>Address</th>
            <th>Country</th>
            <th>Asking Price</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          {data &&
            data.map((h) => (
              <tr key={h.id} onClick={() => nav(`/house/${h.id}`)}>
                <td>{h.address}</td>
                <td>{h.country}</td>
                <td>{currencyFormatter.format(h.price)}</td>
                <td>
                  <button
                    className="primary"
                    onClick={(e) => {
                      e.stopPropagation();
                      nav(`/house/edit/${h.id}`);
                    }}
                  >
                    Edit
                  </button>
                </td>
              </tr>
            ))}
        </tbody>
      </table>
      <Link className="btn btn-primary" to="/house/add">Add</Link>
    </div>
  );
};

export default HouseList;
