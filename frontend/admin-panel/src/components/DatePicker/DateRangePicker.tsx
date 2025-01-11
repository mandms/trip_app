import React, { useEffect, useState } from 'react';
import { DatePicker } from '@mui/x-date-pickers/DatePicker';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { Dayjs } from 'dayjs';

interface DateRangePickerProps {
  onChange: (startDate: Dayjs | undefined, endDate: Dayjs | undefined) => void;
  clear?: boolean;
}

const DateRangePicker: React.FC<DateRangePickerProps> = ({
  onChange,
  clear,
}) => {
  const [startDate, setStartDate] = useState<Dayjs>();
  const [endDate, setEndDate] = useState<Dayjs>();

  useEffect(() => {
    setStartDate(undefined);
    setEndDate(undefined);
  }, [clear]);

  const handleStartDateChange = (date: Dayjs | null) => {
    if (date) {
      setStartDate(date);
      onChange(date, endDate);
    }
  };

  const handleEndDateChange = (date: Dayjs | null) => {
    if (date) {
      setEndDate(date);
      onChange(startDate, date);
    }
  };

  return (
    <LocalizationProvider dateAdapter={AdapterDayjs}>
      <DatePicker
        label="Фильтровать от"
        value={startDate || null}
        sx={{ width: '180px' }}
        slotProps={{ textField: { size: 'small' } }}
        onChange={handleStartDateChange}
      />
      <DatePicker
        label="Фильтровать до"
        value={endDate || null}
        minDate={startDate}
        sx={{ width: '180px' }}
        slotProps={{ textField: { size: 'small' } }}
        onChange={handleEndDateChange}
      />
    </LocalizationProvider>
  );
};

export default DateRangePicker;
