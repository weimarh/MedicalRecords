const NAMESPACE = "medicalRecords";

export const ActionTypes = {
  RECORDS_LOAD: `${NAMESPACE}.medicalRecords.load`,
  RECORDS_CREATE: `${NAMESPACE}.medicalRecords.create`,
  RECORDS_DELETE: `${NAMESPACE}.medicalRecords.delete`,
  RECORDS_UPDATE: `${NAMESPACE}.medicalRecords.update`,
  RECORDS_FILTER: `${NAMESPACE}.medicalRecords.filter`,
  LOADING_SWITCH: `${NAMESPACE}.loading.switch`,
};
