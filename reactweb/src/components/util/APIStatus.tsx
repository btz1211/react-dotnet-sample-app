type Args = {
    status: "idle" | "success" | "error" | "loading"
}

const APIStatus = ({status} : Args) => {
    switch (status) {
        case "error":
            return <div>API communication error</div>
        case "idle":
            return <div>Idle</div>
        case "loading":
            return <div>Loading...</div>
        default:
            throw Error(`Unknown API State ${status}`);
    }
};

export default APIStatus;