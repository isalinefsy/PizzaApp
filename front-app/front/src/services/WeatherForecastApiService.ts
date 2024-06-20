// services/WeatherForecastApiService.ts
import axios, { AxiosInstance } from 'axios';

export interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

const weatherForecastApi: AxiosInstance = axios.create({
  baseURL: 'http://localhost:5227',
  headers: {
    'Content-Type': 'application/json'
  }
});

export default {
  async getAll(): Promise<WeatherForecast[]> {
    const response = await weatherForecastApi.get<WeatherForecast[]>('/weatherforecast');
    return response.data;
  },
  async getById(id: number): Promise<WeatherForecast> {
    const response = await weatherForecastApi.get<WeatherForecast>(`/weatherforecast/${id}`);
    return response.data;
  },
  async create(data: WeatherForecast): Promise<WeatherForecast> {
    const response = await weatherForecastApi.post<WeatherForecast>('/weatherforecast', data);
    return response.data;
  },
  async update(id: number, data: WeatherForecast): Promise<WeatherForecast> {
    const response = await weatherForecastApi.put<WeatherForecast>(`/weatherforecast/${id}`, data);
    return response.data;
  },
  async delete(id: number): Promise<void> {
    await weatherForecastApi.delete(`/weatherforecast/${id}`);
  }
};
