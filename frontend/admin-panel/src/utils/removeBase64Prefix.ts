export const removeBase64Prefix = (image: string) => {
  return image.replace(/^data:image\/\w+;base64,/, '');
};
