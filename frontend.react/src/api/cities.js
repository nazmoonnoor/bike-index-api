import axios from "axios";

export default axios.create({
  baseURL: "https://localhost:7117/api/v1",
});
