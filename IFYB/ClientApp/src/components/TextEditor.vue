<template>
	<editor class="border-radius-lg" :modelValue="modelValue" @update:modelValue="$emit('update:modelValue', $event)" :placeholder="placeholder" :init="{
		height: 300,
		menubar: false,
		images_upload_handler: upload,
		images_upload_base_path: '/',
		plugins: [
			'advlist', 'autolink',
			'lists', 'link', 'image', 'charmap', 'preview', 'anchor', 'searchreplace', 'visualblocks',
			'fullscreen', 'insertdatetime', 'media', 'table', 'help'
		],
		toolbar:
			'undo redo | image | casechange blocks | bold italic backcolor | \
        alignleft aligncenter alignright alignjustify | \
        bullist numlst checklist outdent indent | removeformat | a11ycheck code table help'
	}" />
</template>

<script>
import Editor from '@tinymce/tinymce-vue';
import { useUserAuthentication } from "../store";

export default {
	name: 'TextEditor',
	components: { Editor },
	props: {
		modelValue: String,
		placeholder: String
	},
	emits: ['update:modelValue'],
	setup() {
		const { jwt } = useUserAuthentication();
		const upload = (blobInfo) => new Promise((resolve, reject) => {
			const formData = new FormData();
			formData.append('file', blobInfo.blob(), blobInfo.filename());
			fetch('/api/image', {
        method: 'POST',
        headers: {
          'Authorization': `bearer ${jwt.value}`,
        },        
        body: formData
      }).then(response => {
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
</style>