import React, { useState } from 'react';
import { useQuery, useMutation, useQueryClient } from 'react-query';
import {
  Button,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Paper,
  Box,
  Typography,
  Rating,
} from '@mui/material';
import Pagination from '../../components/Pagination/Pagination';
import ReviewService from '../../api/api.review';
import CreateReviewModal from '../../features/Review/CreateReviewModal';
import UpdateReviewModal from '../../features/Review/UpdateReviewModal';
import { IReview, IReviews } from '../../types/types.review';
import { FilterParamsType, IFilter } from '../../types/types.filter';
import Filter from '../../components/Filter/Filter';

const ReviewPage: React.FC = () => {
  const [page, setPage] = useState<number>(0);
  const [rowsPerPage, setRowsPerPage] = useState(4);
  const [reviewId, setReviewId] = useState<string | null>(null);
  const [reviews, setReviews] = useState<IReviews | null>(null);
  const [filter, setFilter] = useState<IFilter>();
  const [updateReview, setUpdateReview] = useState<IReview | null>(null);
  const [open, setOpen] = useState<{
    create: boolean;
    update: boolean;
    detail: boolean;
  }>({
    create: false,
    update: false,
    detail: false,
  });

  const sortParams = [
    { label: 'Id', value: 'id' },
    { label: 'Текст', value: 'text' },
    { label: 'Рэйтинг', value: 'rate' },
    { label: 'Дата создания', value: 'createdAt' },
  ];
  const filterParams: FilterParamsType = 'date';

  const queryClient = useQueryClient();

  const { data, isLoading, isError, error } = useQuery(
    ['reviews', page, rowsPerPage, filter],
    () => ReviewService.getAll(page + 1, rowsPerPage, filter),
    {
      keepPreviousData: true,
      onSuccess: (reviews) => setReviews(reviews),
    },
  );

  const mutationFilter = useMutation(
    (query: IFilter) => ReviewService.getAll(page + 1, rowsPerPage, query),
    {
      onSuccess: (filteredReviews) => {
        setReviews(filteredReviews);
      },
    },
  );

  const handleUpdateRoute = (
    event: React.MouseEvent<HTMLButtonElement, MouseEvent>,
    review: IReview,
  ) => {
    event.stopPropagation();
    setUpdateReview(review);
    handleOpen('update');
  };

  const mutationDelete = useMutation(ReviewService.delete, {
    onSuccess: () => {
      queryClient.invalidateQueries('reviews');
    },
  });

  const handleDeleteRecord = (
    event: React.MouseEvent<HTMLButtonElement, MouseEvent>,
    id: string,
  ) => {
    event.stopPropagation();
    mutationDelete.mutate(id);
  };

  const handlePageChange = (newPage: number) => {
    setPage(newPage);
  };

  const handleOpen = (modal: string) => {
    setOpen((prevState) => ({
      ...prevState,
      [modal]: true,
    }));
  };

  const handleClose = (modal: string) => {
    setOpen((prevState) => ({
      ...prevState,
      [modal]: false,
    }));
  };

  const handleRowsPerPageChange = (
    event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>,
  ) => {
    setRowsPerPage(parseInt(event.target.value, 10));
    setPage(0);
  };

  const handleRowClick = (id: string) => {
    setReviewId(id);
    setOpen((prevState) => ({
      ...prevState,
      detail: true,
    }));
  };

  const onFilterChange = (query: IFilter) => {
    setFilter(query);
    setPage(0);
    mutationFilter.mutate(query);
  };

  if (isLoading)
    return (
      <div
        style={{
          height: '100%',
          display: 'flex',
          flexDirection: 'column',
          justifyContent: 'center',
        }}
      >
        <Typography textAlign="center" variant="h5">
          Loading...
        </Typography>
      </div>
    );
  if (isError) return <p>Error: {String(error)}</p>;

  return (
    <Box>
      <Paper>
        <Box sx={{ display: 'flex', gap: 2, mb: 2, paddingTop: 2 }}>
          <Button
            onClick={() => handleOpen('create')}
            variant="contained"
            color="success"
            size="small"
            style={{ marginLeft: '10px' }}
          >
            Создать
          </Button>
          <Filter
            filterParams={filterParams}
            sortParams={sortParams}
            onFilterChange={onFilterChange}
          />
        </Box>
        <CreateReviewModal
          open={open.create}
          close={() => handleClose('create')}
        />
        {updateReview && (
          <UpdateReviewModal
            close={() => handleClose('update')}
            open={open.update}
            updatedReview={updateReview}
          />
        )}
        <TableContainer>
          <Table style={{ height: '100%', tableLayout: 'fixed' }}>
            <TableHead>
              <TableRow>
                <TableCell>Текст</TableCell>
                <TableCell>Пользователь</TableCell>
                <TableCell>Дата создания</TableCell>
                <TableCell>Рэйтинг</TableCell>
                <TableCell>Действия</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {data?.data.map((review: IReview) => (
                <TableRow
                  hover
                  sx={{ cursor: 'pointer' }}
                  onClick={() => handleRowClick(review.id)}
                  key={review.id}
                >
                  <TableCell>{review.text}</TableCell>
                  <TableCell>{review.user.username}</TableCell>
                  <TableCell>{review.createdAt}</TableCell>
                  <TableCell>
                    <Rating
                      name="rate"
                      value={review.rate}
                      max={5}
                      readOnly={true}
                    />
                  </TableCell>
                  <TableCell>
                    <div
                      style={{
                        display: 'flex',
                        flexDirection: 'column',
                        gap: '5px',
                        minWidth: '100px',
                      }}
                    >
                      <Button
                        onClick={(event) => handleUpdateRoute(event, review)}
                        variant="contained"
                        color="primary"
                        size="small"
                      >
                        Редактировать
                      </Button>
                      <Button
                        onClick={(event) =>
                          handleDeleteRecord(event, review.id)
                        }
                        variant="contained"
                        color="secondary"
                        size="small"
                      >
                        Удалить
                      </Button>
                    </div>
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

export default ReviewPage;
