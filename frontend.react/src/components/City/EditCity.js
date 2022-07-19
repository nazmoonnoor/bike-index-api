import React from "react";

class EditCity extends React.Component {
  constructor(props) {
    super(props);
    const { id, name, country } = props.location.state.city;
    this.state = {
      id,
      name,
      country,
    };
  }

  update = (e) => {
    e.preventDefault();
    if (this.state.name === "" || this.state.country === "") {
      alert("ALl the fields are mandatory!");
      return;
    }
    this.props.updateCityHandler(this.state);
    this.setState({ name: "", country: "" });
    this.props.history.push("/city-list");
  };
  render() {
    return (
      <div className="ui main">
        <h2>Edit City</h2>
        <form className="ui form" onSubmit={this.update}>
          <div className="field">
            <label>Name</label>
            <input
              type="text"
              name="name"
              placeholder="Name"
              value={this.state.name}
              onChange={(e) => this.setState({ name: e.target.value })}
            />
          </div>
          <div className="field">
            <label>Country</label>
            <input
              type="text"
              name="country"
              placeholder="Country"
              value={this.state.country}
              onChange={(e) => this.setState({ country: e.target.value })}
            />
          </div>
          <button className="ui button blue">Update</button>
        </form>
      </div>
    );
  }
}

export default EditCity;
