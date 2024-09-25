/* eslint-disable react/prop-types */
import Stack from "@mui/material/Stack";
import Button from "@mui/material/Button";

export default function ButtonMUI(props) {
  const { children, onClick, color } = props;
  return (
    <Stack spacing={2} direction="row">
      <Button variant="outlined" onClick={onClick} color={color}>
        {children}
      </Button>
    </Stack>
  );
}
