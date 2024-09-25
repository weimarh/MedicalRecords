/* eslint-disable react/prop-types */
import { PatientProvider } from "./context/patientContext";
import PatientList from "./layouts/PatientList";

export default function Patients(props) {
  const { name } = props;
  return (
    <>
      {name ? (
        <PatientProvider>
          <PatientList />
        </PatientProvider>
      ) : (
        <h1>You are not logged in</h1>
      )}
    </>
  );
}
