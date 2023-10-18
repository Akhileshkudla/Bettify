import { Formik, Field, Form, ErrorMessage } from 'formik';
import { toast } from 'react-toastify';
import { Button, Header } from 'semantic-ui-react';
import * as Yup from 'yup'; // for form validation
import { useStore } from '../stores/store';

export default function ChangePassword() {

  const {userStore} = useStore();
  
  // Define the initial form values
  const initialValues = {
    oldPassword: '',
    newPassword: '',
  };

  // Define the validation schema using Yup
  const validationSchema = Yup.object().shape({
    oldPassword: Yup.string().required('Current password is required'),
    newPassword: Yup.string().required('New password is required'),
  });

  const handleSubmit = (values: any, { setSubmitting, setErrors }: any) => {
    // Handle form submission, e.g., make an API call to update the password
    console.log('Form submitted with values:', values);
    // Add your logic to handle password change here

    userStore.changepassword(values).then(()=> {
      toast.success('Password changed successfully!', values);
      setSubmitting(false);
    })
    .catch((error) => {
      toast.error(error[0])
    });  
    
    setSubmitting(false);
  };

  return (
    <Formik initialValues={initialValues} validationSchema={validationSchema} onSubmit={handleSubmit}>
      {({ isSubmitting, isValid, dirty }) => (
        <Form className='ui form error'>
          <Header as='h2' content='Change Password' color='teal' textAlign='center' />
          <div className='field'>
            <label>Current Password</label>
            <Field type='password' name='oldPassword' placeholder='Enter your current password' />
            <ErrorMessage name='oldPassword' component='div' className='ui pointing red basic label' />
          </div>
          <div className='field'>
            <label>New Password</label>
            <Field type='password' name='newPassword' placeholder='Enter your new password' />
            <ErrorMessage name='newPassword' component='div' className='ui pointing red basic label' />
          </div>
          <ErrorMessage name="error" render={()=> toast.error("Failed") } />
          <Button type='submit' color='teal' fluid size='large' loading={isSubmitting} disabled={!isValid || !dirty || isSubmitting}>
            Change Password
          </Button>          
        </Form>
      )}
    </Formik>
  );
};
