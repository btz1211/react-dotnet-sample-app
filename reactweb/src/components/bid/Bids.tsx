import { useState } from "react";
import { useAddeBid, useFetchBids } from "../../models/BidHooks";
import { House } from "../../models/House";
import { Bid } from "../../models/Bid";
import APIStatus from "../util/APIStatus";
import { currencyFormatter } from "../../config";

type Args = {
    house : House
}

const Bids = ({ house } : Args) => {
    const emptyBid = {
        id: 0,
        houseId: house.id,
        bidder: "",
        amount: 0
    }

    const [bid, setBid] = useState<Bid>(emptyBid);
    const { data, status, isSuccess } = useFetchBids(house.id);
    const addBidMutation = useAddeBid();
    const onSubmit = () => {
        addBidMutation.mutate(bid);
        setBid(emptyBid);
    }

    if (!isSuccess) {
        return <APIStatus status={status} />
    }

    return (
        <>
        <div className="row mt-4">
          <div className="col-12">
            <table className="table table-sm">
              <thead>
                <tr>
                  <th>Bidder</th>
                  <th>Amount</th>
                </tr>
              </thead>
              <tbody>
                {data &&
                  data.map((b) => (
                    <tr key={b.id}>
                      <td>{b.bidder}</td>
                      <td>{currencyFormatter.format(b.amount)}</td>
                    </tr>
                  ))}
              </tbody>
            </table>
          </div>
        </div>
        <div className="row">
          <div className="col-4">
            <input
              id="bidder"
              className="h-100"
              type="text"
              value={bid.bidder}
              onChange={(e) => setBid({ ...bid, bidder: e.target.value })}
              placeholder="Bidder"
            ></input>
          </div>
          <div className="col-4">
            <input
              id="amount"
              className="h-100"
              type="number"
              value={bid.amount}
              onChange={(e) =>
                setBid({ ...bid, amount: parseInt(e.target.value) })
              }
              placeholder="Amount"
            ></input>
          </div>
          <div className="col-2">
            <button
              className="btn btn-primary"
              onClick={() => onSubmit()}
            >
              Add
            </button>
          </div>
        </div>
      </>
    );
}

export default Bids;