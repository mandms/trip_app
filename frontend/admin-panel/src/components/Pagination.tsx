import React from 'react';
import {TablePagination} from '@mui/material';

interface PaginationProps {
    totalItems: number;
    currentPage: number;
    onPageChange: (page: number) => void;
    onRowsPerPageChange: (event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => void;
    rowsPerPage: number
}

const Pagination: React.FC<PaginationProps> = ({ totalItems, currentPage, onPageChange, rowsPerPage, onRowsPerPageChange }) => {
    const pageCount = Math.ceil(totalItems / rowsPerPage);

    return (
        <TablePagination
            component="div"
            count={totalItems}
            page={currentPage}
            rowsPerPage={rowsPerPage}
            onPageChange={(event, page) => onPageChange(page)}
            rowsPerPageOptions={[1, 2, 3, 4]}
            onRowsPerPageChange={onRowsPerPageChange}
        />
    );
};

export default Pagination;
