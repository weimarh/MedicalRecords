/* eslint-disable react/prop-types */
import { createContext, useMemo, useReducer } from "react";
import { patientReducer } from "../context/patientReducer";
import ActionFactory from "./patientActions";

export const PatientContext = createContext();
export const DispatchPatientContext = createContext();

const initialState = {
  patients: [],
  loading: false,
};

export function PatientProvider({ children }) {
  const [patients, dispatch] = useReducer(patientReducer, initialState);

  const actions = useMemo(() => ActionFactory(dispatch), [dispatch]);

  return (
    <PatientContext.Provider value={patients}>
      <DispatchPatientContext value={actions}>
        {children}
      </DispatchPatientContext>
    </PatientContext.Provider>
  );
}
