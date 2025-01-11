export const parseErrorMessage = (error: unknown): string => {
  if (typeof error === 'object' && error !== null) {
    const responseError = (error as any).response?.data;

    if (responseError) {
      if (responseError.title && responseError.detail) {
        return `${responseError.title}: ${responseError.detail}`;
      }

      if (responseError.errors) {
        const validationErrors = Object.entries(responseError.errors)
          .map(
            ([field, messages]) =>
              `${field}: ${(messages as string[]).join(', ')}`,
          )
          .join('; ');
        return validationErrors || 'Ошибка валидации.';
      }
    }

    const errorMessage = (error as any).message || (error as any).detail;
    if (errorMessage) {
      return errorMessage;
    }
  }

  return 'Неизвестная ошибка.';
};
