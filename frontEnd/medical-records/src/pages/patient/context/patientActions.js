import { ActionTypes } from "../../../utils/constants";

function getPatients(dispatch) {
  dispatch({ type: ActionTypes.LOADING_SWITCH, payload: true });

  fetch("")
    .then((res) => res.json())
    .then((data) => {
      dispatch({ type: ActionTypes.RECORDS_LOAD, payload: data });
      dispatch({ type: ActionTypes.LOADING_SWITCH, payload: false });
    })
    .catch((err) => {
      console.log("Error fetching data", err);
      dispatch({ type: ActionTypes.LOADING_SWITCH, payload: false });
    });
}

function createPatient(dispatch, patient) {
  dispatch({ type: ActionTypes.LOADING_SWITCH, payload: true });

  fetch("", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(patient),
  })
    .then((res) => res.json())
    .then((data) => {
      dispatch({ type: ActionTypes.RECORDS_CREATE, payload: data }); //payload: patient
      dispatch({ type: ActionTypes.LOADING_SWITCH, payload: false });
    })
    .catch((err) => {
      console.log("Error creating patient", err);
      dispatch({ type: ActionTypes.LOADING_SWITCH, payload: false });
    });
}

function updatePatient(dispatch, patient) {
  dispatch({ type: ActionTypes.LOADING_SWITCH, payload: true });

  fetch("", {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(patient),
  })
    .then((res) => res.json())
    .then((data) => {
      dispatch({ type: ActionTypes.RECORDS_UPDATE, payload: data }); //payload: patient
      dispatch({ type: ActionTypes.LOADING_SWITCH, payload: false });
    })
    .catch((err) => {
      console.log("Error updating patient", err);
      dispatch({ type: ActionTypes.LOADING_SWITCH, payload: false });
    });
}

function deletePatient(dispatch, id) {
  dispatch({ type: ActionTypes.LOADING_SWITCH, payload: true });

  fetch("", {
    method: "DELETE",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({ id }),
  })
    .then((res) => res.json())
    .then((data) => {
      console.log(data);
      dispatch({ type: ActionTypes.RECORDS_DELETE, payload: id });
      dispatch({ type: ActionTypes.LOADING_SWITCH, payload: false });
    })
    .catch((err) => {
      console.log("Error deleting patient", err);
      dispatch({ type: ActionTypes.LOADING_SWITCH, payload: false });
    });
}

export default function ActionFactory(dispatch) {
  return {
    getPatients: () => getPatients(dispatch),
    createPatient: (patient) => createPatient(dispatch, patient),
    updatePatient: (patient) => updatePatient(dispatch, patient),
    deletePatient: (id) => deletePatient(dispatch, id),
  };
}
