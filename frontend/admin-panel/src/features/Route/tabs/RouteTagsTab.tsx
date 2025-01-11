import RouteTags from '../../../components/RouteTags/RouteTags';
import { Button } from '@mui/material';
import React from 'react';
import { IRouteTag } from '../../../types/types.tag';

interface IRouteTagsTabProps {
  tagList: IRouteTag[];
  selectedTagList: IRouteTag[];
  setTagList: React.Dispatch<React.SetStateAction<IRouteTag[]>>;
  setSelectedTagList: React.Dispatch<React.SetStateAction<IRouteTag[]>>;
  handleRouteChangeTagsSubmit: (e: React.FormEvent) => void;
}

const RouteTagsTab: React.FC<IRouteTagsTabProps> = ({
  tagList,
  setTagList,
  setSelectedTagList,
  selectedTagList,
  handleRouteChangeTagsSubmit,
}) => {
  return (
    <form onSubmit={handleRouteChangeTagsSubmit}>
      <RouteTags
        selectedTagList={selectedTagList}
        setSelectedTagList={setSelectedTagList}
        setTagList={setTagList}
        tagList={tagList}
      />
      <Button
        type="submit"
        variant="contained"
        color="primary"
        fullWidth
        sx={{ marginTop: 2 }}
      >
        Обновить тэги
      </Button>
    </form>
  );
};

export default RouteTagsTab;
