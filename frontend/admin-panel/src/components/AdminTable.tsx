import React, {useState} from 'react';
import {useQuery, useMutation, useQueryClient} from 'react-query';
import {fetchRecords, deleteRecord, createRecord, updateRecord} from '../api/apiClient';
import {
    Button,
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableHead,
    TableRow,
    Paper,
    TextField,
    Box,
    TableFooter
} from '@mui/material';
import Pagination from './Pagination';

interface Record {
    id: string;
    name: string;
    description: string;
}

export interface Props {
    table: string
}

const AdminTable: React.FC<Props> = (props: Props) => {
    const [page, setPage] = useState(0);
    const [rowsPerPage, setRowsPerPage] = useState(1);
    const [isEditMode, setIsEditMode] = useState(false);
    const [currentRecord, setCurrentRecord] = useState<Record | null>(null);

    const queryClient = useQueryClient();

    // Получение списка записей с пагинацией
    const {data, isLoading, isError, error} = useQuery(['records', page, rowsPerPage], () => fetchRecords(page + 1, rowsPerPage), {
        keepPreviousData: true,
    });

    // Мутации для создания, обновления и удаления записей
    const mutationCreate = useMutation(createRecord, {
        onSuccess: () => {
            queryClient.invalidateQueries('records');
        },
    });

    const mutationUpdate = useMutation((record: Record) => updateRecord(record.id, record), {
        onSuccess: () => {
            queryClient.invalidateQueries('records');
        },
    });

    const mutationDelete = useMutation(deleteRecord, {
        onSuccess: () => {
            queryClient.invalidateQueries('records');
        },
    });

    // Обработчики CRUD операций
    const handleAddRecord = () => {
        if (currentRecord) {
            mutationCreate.mutate(currentRecord);
            setCurrentRecord(null);
        }
    };

    const handleUpdateRecord = () => {
        if (currentRecord) {
            mutationUpdate.mutate(currentRecord);
            setIsEditMode(false);
            setCurrentRecord(null);
        }
    };

    const handleDeleteRecord = (id: string) => {
        mutationDelete.mutate(id);
    };

    const handleEditRecord = (record: Record) => {
        setCurrentRecord(record);
        setIsEditMode(true);
    };

    const handlePageChange = (newPage: number) => {
        setPage(newPage);
    };

    const handleRowsPerPageChange = (event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        setRowsPerPage(parseInt(event.target.value, 10));
        setPage(0);
    };

    if (isLoading) return <p>Loading...</p>;
    if (isError) return <p>Error: {String(error)}</p>;

    return (
        <Box height="100%">
            <Paper>
                <Box mb={2}>
                    {isEditMode ? (
                        <>
                            <TextField
                                label="Title"
                                value={currentRecord?.name || ''}
                                onChange={(e) => setCurrentRecord({...currentRecord!, name: e.target.value})}
                                fullWidth
                                margin="normal"
                            />
                            <TextField
                                label="Description"
                                value={currentRecord?.description || ''}
                                onChange={(e) => setCurrentRecord({...currentRecord!, description: e.target.value})}
                                fullWidth
                                margin="normal"
                            />
                            <Button onClick={handleUpdateRecord} variant="contained" color="primary">
                                Обновить
                            </Button>
                        </>
                    ) : (
                        <>
                            <TextField
                                label="Title"
                                onChange={(e) => setCurrentRecord({...currentRecord!, name: e.target.value})}
                                fullWidth
                                margin="normal"
                            />
                            <TextField
                                label="Description"
                                onChange={(e) => setCurrentRecord({...currentRecord!, description: e.target.value})}
                                fullWidth
                                margin="normal"
                            />
                            <Button onClick={handleAddRecord} variant="contained" color="primary">
                                Добавить
                            </Button>
                        </>
                    )}
                </Box>

                <TableContainer>
                    <Table style={{height: "100%"}}>
                        <TableHead>
                            <TableRow>
                                <TableCell>Title</TableCell>
                                <TableCell>Description</TableCell>
                                <TableCell>Actions</TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {data.data.map((record: Record) => (
                                <TableRow key={record.id}>
                                    <TableCell>{record.name}</TableCell>
                                    <TableCell>{record.description}</TableCell>
                                    <TableCell>
                                        <Button onClick={() => handleEditRecord(record)} variant="contained"
                                                color="primary" size="small">
                                            Редактировать
                                        </Button>
                                        <Button onClick={() => handleDeleteRecord(record.id)} variant="contained"
                                                color="secondary" size="small"
                                                style={{marginLeft: "10px"}}
                                        >
                                            Удалить
                                        </Button>
                                    </TableCell>
                                </TableRow>
                            ))}
                        </TableBody>
                    </Table>
                </TableContainer>
                <Pagination
                    totalItems={data?.totalRecords || 0}
                    currentPage={page}
                    onPageChange={handlePageChange}
                    rowsPerPage={rowsPerPage}
                    onRowsPerPageChange={handleRowsPerPageChange}
                />
            </Paper>
        </Box>
    );
};

export default AdminTable;
