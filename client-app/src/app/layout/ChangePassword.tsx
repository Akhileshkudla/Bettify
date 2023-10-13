import { Formik, Field, Form, ErrorMessage } from 'formik';
import { Button, Header } from 'semantic-ui-react';
import * as Yup from 'yup'; // for form validation

export default function ChangePassword() {
  // Define the initial form values
  const initialValues = {
    email: '',
    newPassword: '',
  };

  // Define the validation schema using Yup
  const validationSchema = Yup.object().shape({
    email: Yup.string().email('Invalid email address').required('Email is required'),
    newPassword: Yup.string().required('New password is required'),
  });

  const handleSubmit = (values: any, { setSubmitting }: any) => {
    // Handle form submission, e.g., make an API call to update the password
    console.log('Form submitted with values:', values);
    // Add your logic to handle password change here
    setSubmitting(false);
  };

  return (
    <Formik initialValues={initialValues} validationSchema={validationSchema} onSubmit={handleSubmit}>
      {({ isSubmitting }) => (
        <Form className='ui form error'>
          <Header as='h2' content='Change Password' color='teal' textAlign='center' />
          <div className='field'>
            <label>Email</label>
            <Field type='email' name='email' placeholder='Enter your email' />
            <ErrorMessage name='email' component='div' className='ui pointing red basic label' />
          </div>
          <div className='field'>
            <label>New Password</label>
            <Field type='password' name='newPassword' placeholder='Enter your new password' />
            <ErrorMessage name='newPassword' component='div' className='ui pointing red basic label' />
          </div>
          <Button type='submit' color='teal' fluid size='large' loading={isSubmitting}>
            Change Password
          </Button>
        </Form>
      )}
    </Formik>
  );
};
