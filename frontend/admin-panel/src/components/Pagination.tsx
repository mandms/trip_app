import React from 'react';
import { LabelDisplayedRowsArgs, TablePagination } from '@mui/material';

interface PaginationProps {
  totalItems: number;
  currentPage: number;
  onPageChange: (page: number) => void;
  onRowsPerPageChange: (
    event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>,
  ) => void;
  rowsPerPage: number;
}

const Pagination: React.FC<PaginationProps> = ({
  totalItems,
  currentPage,
  onPageChange,
  rowsPerPage,
  onRowsPerPageChange,
}) => {
  const labelDisplayedRows = ({ from, to, count }: LabelDisplayedRowsArgs) =>
    `${from}–${to} из ${count !== -1 ? count : `больше чем ${to}`}`;

  return (
    <TablePagination
      component="div"
      count={totalItems}
      page={currentPage}
      labelRowsPerPage={'Кол-во записей на странице:'}
      labelDisplayedRows={labelDisplayedRows}
      rowsPerPage={rowsPerPage}
      onPageChange={(event, page) => onPageChange(page)}
      rowsPerPageOptions={[4, 6, 8, 10]}
      onRowsPerPageChange={onRowsPerPageChange}
    />
  );
};

export default Pagination;
