import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import Paper from "@mui/material/Paper";
import { useContext, useEffect } from "react";
import {
  PatientContext,
  DispatchPatientContext,
} from "../context/patientContext";
import ButtonMUI from "../../../components/button/Button";

export default function PatientList() {
  const { patients, loading } = useContext(PatientContext);
  const actions = useContext(DispatchPatientContext);

  useEffect(() => {
    actions.getPatients();
  }, [actions]);

  return (
    <TableContainer component={Paper}>
      <Table sx={{ minWidth: 650 }} size="small" aria-label="a dense table">
        <TableHead>
          <TableRow>
            <TableCell>Id</TableCell>
            <TableCell align="right">First Name</TableCell>
            <TableCell align="right">Last Name</TableCell>
            <TableCell align="right">Birthdate(g)</TableCell>
            <TableCell align="right">Gender</TableCell>
            <TableCell align="right">Actions</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {loading && <p>Loading...</p>}
          {patients.map((row) => (
            <TableRow
              key={row.name}
              sx={{ "&:last-child td, &:last-child th": { border: 0 } }}
            >
              <TableCell component="th" scope="row">
                {row.name}
              </TableCell>
              <TableCell align="right">{row.id}</TableCell>
              <TableCell align="right">{row.firstName}</TableCell>
              <TableCell align="right">{row.lastName}</TableCell>
              <TableCell align="right">{row.birthDate}</TableCell>
              <ButtonMUI>Update</ButtonMUI>
              <ButtonMUI color="error">Delete</ButtonMUI>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
}
