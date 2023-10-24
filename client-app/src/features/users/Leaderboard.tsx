import { observer } from 'mobx-react-lite';
import { useEffect } from 'react';
import { Header, Table } from 'semantic-ui-react';
import { useStore } from '../../app/stores/store';
import {
  Chart as ChartJS,
  CategoryScale,
  LinearScale,
  BarElement,
  Title,
  Tooltip,
  Legend,
} from 'chart.js';
import { Bar } from 'react-chartjs-2';

ChartJS.register(
  CategoryScale,
  LinearScale,
  BarElement,
  Title,
  Tooltip,
  Legend
);

export const options = {
  indexAxis: 'y' as const,
  elements: {
    bar: {
      borderWidth: 2,
      radius: 5,
    },
  },
  responsive: true,
  plugins: {
    legend: {
      position: 'bottom' as const,
    },
    title: {
      display: true,      
      text: 'Leaderboard',
      font: {
        size: 20,
        family: 'Public Sans, sans-serif'
      },
      padding: {
          top: 15,
          bottom: 15
      }
    },
  },
};

export default observer (function UserForm() {
    const {userStore } = useStore();
    const {users} = userStore;

    useEffect(() =>{
        userStore.getallusers()
    }, [userStore])

      // Sort the users array in decreasing order of 'amount'
  const sortedUsers = users.slice().sort((a, b) => b.amount - a.amount);

  const filteredUsers = users.filter((user) => user.username !== 'admin')
  const labels = filteredUsers.map((user) => user.displayName);
  const data = filteredUsers.map((user) => user.amount);

  const barData = {
    labels: labels,
    datasets: [
      {
        label: 'User Amounts',
        data: data,
        borderColor: createGradient(),
        backgroundColor: createGradient(), // Customize the border color
        borderWidth: 1,        
      },
    ],
  };

  return (
    <>
    <Bar color='teal' options={options} data={barData} />
    <Table celled>
      <Table.Header>
        <Table.Row>
            <Table.HeaderCell>Rank</Table.HeaderCell>
          <Table.HeaderCell>Username</Table.HeaderCell>
          <Table.HeaderCell>Amount</Table.HeaderCell>
        </Table.Row>
      </Table.Header>
      <Table.Body>
        {sortedUsers.map((user, index) => (
          <Table.Row key={user.username}>
            <Table.Cell>{index + 1}</Table.Cell>
            <Table.Cell>
                <Header.Content>
                    {user.displayName}
                    <Header.Subheader>{user.username}</Header.Subheader>
                </Header.Content>
            </Table.Cell>
            <Table.Cell >{user.amount}</Table.Cell>            
          </Table.Row>
        ))}
      </Table.Body>
    </Table>
    </>
  );
})

function createGradient(): CanvasGradient {
  const canvas = document.createElement('canvas');
  const ctx = canvas.getContext('2d');

  const gradient = ctx!.createLinearGradient(0, 0, canvas.width *2, canvas.height);
  gradient.addColorStop(0, 'red');
  gradient.addColorStop(1, '#f5a640');

  return gradient;
}