import { ActionTypes } from "../../../utils/constants";

export function patientReducer(state, action) {
  const { type, payload } = action;

  switch (type) {
    case ActionTypes.LOADING_SWITCH:
      return {
        ...state,
        loading: payload,
      };
    case ActionTypes.RECORDS_LOAD:
      return {
        ...state,
        patients: payload,
      };
    case ActionTypes.RECORDS_CREATE:
      return {
        ...state,
        patients: [...state.patients, payload],
      };
    case ActionTypes.RECORDS_UPDATE:
      return {
        ...state,
        patients: state.patients.map((patient) =>
          patient.id === payload.id ? payload : patient
        ),
      };
    case ActionTypes.RECORDS_DELETE:
      return {
        ...state,
        patients: state.patients.filter((patient) => patient.id !== payload),
      };
    default:
      return state;
  }
}
