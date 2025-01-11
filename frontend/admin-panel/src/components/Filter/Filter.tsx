import {
  FormControl,
  InputLabel,
  MenuItem,
  Select,
  TextField,
  Box,
  SelectChangeEvent,
  Chip,
} from '@mui/material';
import React, { useState } from 'react';
import {
  DateRange,
  FilterParamsType,
  IFilter,
  OrderType,
} from '../../types/types.filter';
import { useQuery } from 'react-query';
import TagService from '../../api/api.tag';
import { ITags } from '../../types/types.tag';
import DateRangePicker from '../DatePicker/DateRangePicker';
import dayjs from 'dayjs';
import { ArrowDownward, ArrowUpward } from '@mui/icons-material';
import DeleteIcon from '@mui/icons-material/Delete';

interface IFilterProps {
  filterParams?: FilterParamsType;
  sortParams: { label: string; value: string }[];
  onFilterChange: (query: IFilter) => void;
}

const Filter: React.FC<IFilterProps> = ({
  onFilterChange,
  filterParams,
  sortParams,
}) => {
  const [search, setSearch] = useState<string | undefined>();
  const [sort, setSort] = useState<string | undefined>();
  const [tag, setTag] = useState<string | undefined>();
  const [order, setOrder] = useState<OrderType>('asc');
  const [tags, setTags] = useState<ITags | null>(null);
  const [dateRange, setDateRange] = useState<DateRange>();
  const [clear, setClear] = useState<boolean>();

  useQuery(['filterTags'], () => TagService.getAll(), {
    onSuccess: (tags) => {
      setTags(tags);
    },
    keepPreviousData: true,
    enabled: filterParams === 'tag',
  });

  const handleSearchChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const newSearch = event.target.value;
    setSearch(newSearch);
    onFilterChange({
      search: newSearch,
      sort: sort,
      order: order,
      tagFilter: tag,
      dateFilter: {
        start: dateRange?.start,
        end: dateRange?.end,
      },
    });
  };

  const handleSortChange = (event: SelectChangeEvent) => {
    const newSort = event.target.value as string;
    setSort(newSort);
    onFilterChange({
      search: search,
      sort: newSort,
      order: order,
      tagFilter: tag,
      dateFilter: {
        start: dateRange?.start,
        end: dateRange?.end,
      },
    });
  };

  const handleTagChange = (event: SelectChangeEvent) => {
    const newTag = event.target.value as string;
    setTag(newTag);
    onFilterChange({
      search: search,
      sort: sort,
      order: order,
      tagFilter: newTag,
      dateFilter: {
        start: dateRange?.start,
        end: dateRange?.end,
      },
    });
  };

  const handleDateChange = (
    startDate: dayjs.Dayjs | undefined,
    endDate: dayjs.Dayjs | undefined,
  ) => {
    const newDateRange = {
      start: startDate?.format('YYYY-MM-DD').toString(),
      end: endDate?.format('YYYY-MM-DD').toString(),
    };
    setDateRange(newDateRange);
    onFilterChange({
      search: search,
      sort: sort,
      order: order,
      tagFilter: tag,
      dateFilter: newDateRange,
    });
  };

  const handleToggle = () => {
    const newOrder: OrderType = order === 'asc' ? 'desc' : 'asc';
    setOrder(newOrder);

    if (!sort) return;

    onFilterChange({
      search: search,
      sort: sort,
      order: newOrder,
      tagFilter: tag,
      dateFilter: {
        start: dateRange?.start,
        end: dateRange?.end,
      },
    });
  };

  const handleClear = () => {
    if (!sort && !search && !tag && !dateRange && order === 'asc') return;

    setOrder('asc');
    setSort(undefined);
    setSearch(undefined);
    setTag(undefined);
    setDateRange(undefined);
    setClear((prev) => !prev);
    onFilterChange({
      search: undefined,
      sort: undefined,
      order: undefined,
      tagFilter: undefined,
      dateFilter: undefined,
    });
  };

  return (
    <Box display="flex" gap={2} flexWrap="wrap" alignItems="center">
      <TextField
        label="Поиск"
        variant="outlined"
        value={search || ''}
        size="small"
        onChange={handleSearchChange}
      />

      {tags && (
        <FormControl variant="outlined" size="small" sx={{ minWidth: 180 }}>
          <InputLabel>Фильтрация</InputLabel>
          <Select value={tag || ''} onChange={handleTagChange} label="Filter">
            <MenuItem value={''}>Без фильтра</MenuItem>
            {tags.data.map((tag) => (
              <MenuItem key={tag.id} value={tag.name}>
                {tag.name}
              </MenuItem>
            ))}
          </Select>
        </FormControl>
      )}

      {filterParams === 'date' && (
        <DateRangePicker clear={clear} onChange={handleDateChange} />
      )}

      <FormControl variant="outlined" size="small" sx={{ minWidth: 180 }}>
        <InputLabel>Сортировка</InputLabel>
        <Select value={sort || ''} onChange={handleSortChange} label="Sort">
          <MenuItem value={''}>Без сортировки</MenuItem>
          {sortParams.map((param) => (
            <MenuItem key={param.value} value={param.value}>
              {param.label}
            </MenuItem>
          ))}
        </Select>
      </FormControl>

      <Chip
        color="primary"
        onClick={handleToggle}
        label={order === 'asc' ? 'По возрастанию' : 'По убыванию'}
        icon={order === 'asc' ? <ArrowUpward /> : <ArrowDownward />}
      />

      <Chip
        color="warning"
        onClick={handleClear}
        label={'Сбросить'}
        icon={<DeleteIcon />}
      />
    </Box>
  );
};

export default Filter;
