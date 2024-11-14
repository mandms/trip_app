import axios from 'axios';

const apiClient = axios.create({
    baseURL: 'http://localhost:5218/api',  // Замените на ваш API
    headers: {
        'Content-Type': 'application/json',
    },
});

// Получение данных с пагинацией
export const fetchRecords = async (page: number, limit: number) => {
    const { data } = await apiClient.get('/route', {
        params: { PageNumber: page, PageSize: limit },
    });
    return data;
};

// Получение одной записи по ID
export const fetchRecordById = async (id: string) => {
    const { data } = await apiClient.get(`/records/${id}`);
    return data;
};

// Создание новой записи
export const createRecord = async (record: any) => {
    const { data } = await apiClient.post('/records', record);
    return data;
};

// Обновление записи
export const updateRecord = async (id: string, record: any) => {
    const { data } = await apiClient.put(`/records/${id}`, record);
    return data;
};

// Удаление записи
export const deleteRecord = async (id: string) => {
    const { data } = await apiClient.delete(`/records/${id}`);
    return data;
};
