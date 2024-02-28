import React from 'react';
import BlogPostItem from '@theme-original/BlogPostItem';
import { useBlogPost } from '@docusaurus/theme-common/internal'
import useIsBrowser from '@docusaurus/useIsBrowser';
import GiscusComponent from '@site/src/components/GiscusComponent/GiscusComponent';

export default function BlogPostItemWrapper(props) {
  const { metadata, isBlogPostPage } = useBlogPost()
  const isBrowser = useIsBrowser();

  const { frontMatter, title } = metadata
  const { enableComments } = frontMatter

  return (
    <>
      <BlogPostItem {...props} />
      {(enableComments && isBlogPostPage) && (
        <GiscusComponent />
      )}
    </>
  );
}
