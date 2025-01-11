import { Box, Button, Grid2, Typography } from '@mui/material';
import React from 'react';
import { IRouteTag } from '../../types/types.tag';

interface IRouteTagsProps {
  tagList: IRouteTag[];
  selectedTagList: IRouteTag[];
  setTagList: React.Dispatch<React.SetStateAction<IRouteTag[]>>;
  setSelectedTagList: React.Dispatch<React.SetStateAction<IRouteTag[]>>;
}

const RouteTags: React.FC<IRouteTagsProps> = ({
  tagList,
  setTagList,
  setSelectedTagList,
  selectedTagList,
}) => {
  const handleTagClick = (tag: IRouteTag, index: number) => {
    setTagList((prev) => prev.filter((_, i) => i !== index));
    setSelectedTagList((prevSelected) => [...prevSelected, tag]);
  };

  const handleSelectedTagClick = (tag: IRouteTag, index: number) => {
    setTagList((prevSelected) => [...prevSelected, tag]);
    setSelectedTagList((prev) => prev.filter((_, i) => i !== index));
  };

  return (
    <Box>
      <Box mt={2} minWidth={'100%'}>
        <Typography variant="h6">Тэги:</Typography>
        <Grid2 container spacing={1}>
          {!!tagList.length &&
            tagList.map((tag, index) => (
              <Grid2 key={tag.id}>
                <Button
                  variant="outlined"
                  size="small"
                  onClick={() => handleTagClick(tag, index)}
                >
                  {tag.name}
                </Button>
              </Grid2>
            ))}
        </Grid2>
      </Box>
      <Box mt={2}>
        {!!selectedTagList.length && (
          <Typography variant="h6">Выбранные тэги:</Typography>
        )}
        <Grid2 container spacing={1}>
          {!!selectedTagList.length &&
            selectedTagList.map((tag, index) => (
              <Grid2 key={tag.id}>
                <Button
                  variant="outlined"
                  size="small"
                  onClick={() => handleSelectedTagClick(tag, index)}
                >
                  {tag.name}
                </Button>
              </Grid2>
            ))}
        </Grid2>
      </Box>
    </Box>
  );
};

export default RouteTags;
