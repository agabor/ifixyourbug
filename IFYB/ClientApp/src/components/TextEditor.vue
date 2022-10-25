<template>
	<editor class="border-radius-lg" :modelValue="modelValue" @update:modelValue="$emit('update:modelValue', $event)" :placeholder="placeholder" :init="{
		height: 300,
		menubar: false,
		images_upload_handler: upload,
		images_upload_base_path: '/',
		plugins: [
			'advlist', 'autolink',
			'lists', 'link', 'image', 'searchreplace',
			'fullscreen', 'insertdatetime', 'media', 'table'
		],
		toolbar:
			'undo redo | image | casechange blocks | bold italic backcolor | \
        alignleft aligncenter alignright alignjustify | \
        bullist numlst checklist removeformat code table fullscreen'
	}" />
</template>

<script>
import Editor from '@tinymce/tinymce-vue';
import { useUserAuthentication } from "../store/client";

export default {
	name: 'TextEditor',
	components: { Editor },
	props: {
		modelValue: String,
		placeholder: String
	},
	emits: ['update:modelValue'],
	setup() {
		const { postData } = useUserAuthentication();
		const upload = (blobInfo) => new Promise((resolve, reject) => {
			const formData = new FormData();
			formData.append('file', blobInfo.blob(), blobInfo.filename());
			postData('/api/image', formData).then(response => {
				response.json().then(data => {
					resolve(data.location)
				})
			}).catch(reject)
		});
		return { upload }
	}
}
</script>

<style>
  .tox-statusbar {
    display: none !important;
  }
  .tox.tox-tinymce.tox-fullscreen {
    position: absolute!important;
    width: 100%!important;
    height: calc(100vh - 10vh)!important;
  }
  img {
    max-width: 100%;
  }
</style>